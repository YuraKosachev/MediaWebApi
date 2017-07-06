
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using System.Collections.Generic;
using MediaStoreApi.Services.Interfaces;
using MediaStoreApi.Infrastructure.FileManage;
using MediaStoreApi.Domain.Interfaces;
using MediaStoreApi.Infrastructure.Business;
using MediaStoreApi.Infrastructure.XML;
using MediaStoreApi.Infrastructure.Data;
//using MediaStoreApi.ExtensionLibraries.Images;
using MediaStoreApi.Extension.MediaFileOperations;
using System.Web.Http;
using System.Web.Configuration;


namespace MediaStoreApi
{
    public class AutofacConfig
    {
        public static void Configure()
        {

            //Autofac
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            RegisterType(builder,new WebConfigSettings());
            builder.RegisterWebApiFilterProvider(config);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());

        }
        private static void RegisterType(ContainerBuilder builder, WebConfigSettings settings)
        { 

            builder.RegisterType<MediaFolderManager>().As<IFolderManager>()
                .WithParameters(new List<NamedParameter> {
                    new NamedParameter("mediaFolder", HttpContext.Current.Server.MapPath(settings.GetStringSetting("MediaStoreFolder"))),
                    new NamedParameter("thumbnailFolderName", settings.GetStringSetting("ThumbnailFolderName")),
                    new NamedParameter("holderFolder", settings.GetStringSetting("HoldersFolderName")),
                    new NamedParameter("unknownFileName", settings.GetStringSetting("UnknownMediaFileName"))

                });

            builder.RegisterType<MediaStoreService>().As<IMediaService>();
                

            builder.RegisterType<MediaStoreFileProvider>().As<IMediaFileProvider>();


            //setting  mediafileoperations module

            var width = settings.GetIntOrDefaultSetting("ThumbnailImageWidth", 100);
            var height = settings.GetIntOrDefaultSetting("ThumbnailImageHeight", 100);
            var operations = new Dictionary<string, IMediaFileOperations>();
            //the key is a pattern on which a search provider for processing the incoming media type
            operations.Add("image", new ImageFileOperations(width, height));
           // operations.Add("video", new VideoFileOperations());
            builder.RegisterType<MediaFileOperationsSwitch>().As<IMediaFileOperationsSwitch>()
                .WithParameter(new NamedParameter("operationProviders", operations));
               



            //uncomment this if you wont to use xml file as store
            builder.RegisterType<MediaStoreApiXmlProvider>().As<IMediaInfoProvider>()
             .WithParameter(new NamedParameter("xmlFileName", settings.GetStringSetting("XmlStoreInfoFileName")));

            //uncomment this if you wont to use database  as store
           // builder.RegisterType<MediaStoreDbProvider>().As<IMediaInfoProvider>();



        }
    }
    
}