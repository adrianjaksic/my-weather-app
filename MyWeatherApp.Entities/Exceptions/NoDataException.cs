using System;

namespace MyWeatherApp.Entities.Exceptions
{
    public class NoDataException : BaseException
    {
        public NoDataException() : base("No data")
        {

        }
    }
}
