﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAspNet5.Hypermedia.Contants
{
    public sealed class RelationType
    {
        public const string self = "self";
        public const string post = "post";

        public const string put = "put";
        public const string delete = "delete";
        public const string path = "path";

        public const string next = "next";
        public const string previous = "previous";
        public const string first = "first";
        public const string last = "last";
    }
}