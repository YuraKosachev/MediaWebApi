using System;


namespace MediaStoreApi.Domain.Exceptions
{
    public class BadRequestValues:Exception
    {
        public BadRequestValues(string message):base(message)
        { }
    }
}
