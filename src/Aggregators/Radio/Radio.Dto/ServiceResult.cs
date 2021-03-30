using System;

namespace Radio.Dto
{
    public class ServiceResult<T>
    {
        public ServiceResult(bool ok, T result)
        {
            Ok = ok;
            Result = result;
        }

        public ServiceResult(T result)
        {
            Ok = true;
            Result = result;
        }

        public bool Ok { get; set; }
        public T Result { get; set; }
    }
}
