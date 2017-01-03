/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class ResourceTest
    {
        protected class Inner : Resource
        {
            public string Qux { get; set; }
        }

        protected class Data : Resource
        {
            public string Foo { get; set; }
            public int Bar { get; set; }
            public List<Inner> Baz { get; set; }
        }

        [TestClass]
        public class ResourceExtenstionTest
        {
            private Data _source;

            [TestInitialize]
            public void Initialize()
            {
                _source = new Data { Foo = "oof", Bar = 42, Baz = new List<Inner> { new Inner { Qux = "xuq" } } };
            }

            [TestMethod]
            public void TestAsDictionary()
            {
                var dictionary = _source.AsDictionary();

                Assert.AreEqual(dictionary["foo"], "oof");
                Assert.AreEqual(dictionary["bar"], 42);
                Assert.AreEqual(((List<Dictionary<string, object>>)dictionary["baz"])[0]["qux"], "xuq");
            }
        }
    }
}