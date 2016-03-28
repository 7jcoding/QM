using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest {
    [TestClass]
    public class AbstrictClassJsonTest {

        [TestMethod]
        public void Test() {
            var stockholder = new Stockholder {
                FullName = "Steve Stockholder",
                Businesses = new List<Business>{
                                    new Hotel{
                                        Name = "Hudson Hotel",
                                        Stars = 4
                                    },
                                    new Product() {
                                        Name = "Book",
                                        ProductID = 1
                                    }
                },
                User = new User() {
                    Name = "xling",
                    Pwd = "xling"
                }
            };

            var settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(stockholder, Formatting.Indented, settings);

            var o = JsonConvert.DeserializeObject<Stockholder>(json, settings);
        }

    }


    public abstract class Business {
        public string Name { get; set; }
    }

    public class Hotel : Business {
        public int Stars { get; set; }
    }

    public class Product : Business {
        public int ProductID { get; set; }
    }

    public class Stockholder {
        public string FullName { get; set; }
        public IList<Business> Businesses { get; set; }
        public User User { get; set; }
    }

    public class User {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
}
