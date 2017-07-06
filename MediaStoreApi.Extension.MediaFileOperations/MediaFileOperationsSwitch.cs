using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaStoreApi.Domain.Interfaces;
using MediaStoreApi.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace MediaStoreApi.Extension.MediaFileOperations
{
    public class MediaFileOperationsSwitch : IMediaFileOperationsSwitch
    {
        private IDictionary<string, IMediaFileOperations> _operations;
       
        public MediaFileOperationsSwitch(IDictionary<string, IMediaFileOperations> operationProviders)
        {
            _operations = operationProviders;
        }

        public IMediaFileOperations this[string key] {

            get {

                return GetProvider(key);
            }
        }
        private IMediaFileOperations GetProvider(string mediaType)
        {
            string key;
            return IsKeyExist(mediaType, out key) ? _operations[key] : null;
        }
        private bool IsKeyExist(string mediaType,out string key)
        {

            key = _operations.Keys.FirstOrDefault(k => {
                return new Regex(k).IsMatch(mediaType);
            });

            return key != null;
        }
    }
}
