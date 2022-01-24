﻿namespace Common.Abstractions
{
    public interface IWriter<in T>
    {
        void Write(T data);
    }
}
