﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstraintOnTypeParameters
{
    class StructArray<T> where T : struct // 값 형식
    {
        public T[] Array { get; set; }
        public StructArray(int size)
        {
            Array = new T[size];
        }
    }

    class RefArray<T> where T : class // 참조 형식
    {
        public T[] Array { get; set; }
        public RefArray(int size)
        {
            Array = new T[size];
        }
    }

    class Base { }
    class Derived : Base { }
    class BaseArray<U> where U : Base
    {
        public U[] Array { get; set; }
        public BaseArray(int size)
        {
            Array = new U[size];
        }

        public void CopyArray<T>(T[] Source) where T : U
        {
            Source.CopyTo(Array, 0);
        }
    }
    
    class Program
    {
        public static T CreateInstance<T>() where T : new()
        {
            return new T();
        }

        static void Main(string[] args)
        {
            StructArray<int> a = new StructArray<int>(3);
            a.Array[0] = 0;

            RefArray<StructArray<double>> b = new RefArray<StructArray<double>>(3);
            b.Array[0] = new StructArray<double>(5);

            BaseArray <Base> c = new BaseArray<Base>(3);
            c.Array[0] = new Base();
            c.Array[1] = new Derived();
            c.Array[2] = CreateInstance<Base>();

            BaseArray<Derived> d = new BaseArray<Derived>(3);
            d.Array[0] = new Derived(); // Base형식은 여기에 할당 불가
            d.Array[1] = CreateInstance<Derived>();
  
            BaseArray<Derived> e = new BaseArray<Derived>(3);
            e.CopyArray<Derived>(d.Array);
        }
    }
}
