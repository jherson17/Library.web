﻿namespace Library.Web.Core
{
    public class Response<T>
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }
    }
}
