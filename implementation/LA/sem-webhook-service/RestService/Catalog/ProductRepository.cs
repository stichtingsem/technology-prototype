using IO.Swagger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestService.Catalog
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = null;

        private List<Product> GetAllProducts
        {
            get
            {
                if (_products == null)
                {
                    _products = new List<Product>();
                    _products.Add(NectarV4());
                    _products.Add(NectarV5());
                    _products.Add(NectarV6());
                }

                return _products;
            }
        }

        private static Product NectarV6()
        {
            string name = "Nectar 4e ed leerjaar v 6 volledig digitaal";
            string ean = "8717927128718";
            string level = "vwo-6";
            string imageUrl = "https://images.ctfassets.net/huogrpkfou0w/43Z1MnfsBxGbn3oOCwJ1po/0cab347b648af96177df1f9f980f1a5c/9789001789398.png";

            return GetNewProduct(name, ean, level, imageUrl);
        }
        private static Product NectarV5()
        {
            string name = "Nectar 4e ed leerjaar 5 volledig digitaal";
            string ean = "8717927120323";
            string level = "vwo-5";
            string imageUrl = "https://images.ctfassets.net/huogrpkfou0w/59gvnlEkTSuOZdrkexh7Uk/9386d1fb6eb859a955330285def3adcd/9789001885908.png";

            return GetNewProduct(name, ean, level, imageUrl);
        }
        private static Product NectarV4()
        {
            string name = "Nectar 4e ed leerjaar 4 volledig digitaal";
            string ean = "8717927098820";
            string level = "vwo-4";
            string imageUrl = "https://images.ctfassets.net/huogrpkfou0w/6se2734CDovnxdFRELkjcJ/b00f6b58d2de6e253308fe3a2fd30646/9789001885960.png";

            return GetNewProduct(name, ean, level, imageUrl);
        }

        private static Product NectarV3()
        {
            string name = "Nectar 5e ed vwo 2-3 leerboek";
            string ean = "9789001880521";
            string level = "vwo-3";
            string imageUrl = "https://images.ctfassets.net/huogrpkfou0w/7kTdxBFq1GCOsFJUzArj3J/7c98413efb3b0e8d3c0b98a166e9fe30/9789001880521.png";

            var prd = GetNewProduct(name, ean, level, imageUrl);

            prd.LevelSubjects.Add(new LevelSubjects()
            {
                Level = "vwo-2",
                SubjectCode = "vwo-2"
            });

            return prd;
        }


        private static Product GetNewProduct(string name, string ean, string level, string imageUrl)
        {
            return new Product()
            {
                Ean = ean,
                Type = Product.TypeEnum.DigitalEnum,
                Status = Product.StatusEnum.ActiveEnum,
                Name = name,
                Streams = new List<Stream>()
                        {
                            new Stream()
                            {
                                Id = "1",
                                Name = "Biologie",
                                Description = "Biologie"
                            }
                        },
                LevelSubjects = new List<LevelSubjects>()
                        {
                            new LevelSubjects()
                            {
                                Level = level,
                                SubjectCode = level
                            }
                        },
                Models = new List<BusinessModel>()
                        {
                            new BusinessModel()
                            {
                                Id = "1",
                                Name = name,
                                Description = name,
                                Price =Convert.ToDecimal( 29.25),
                                PricePeriod = "year",
                                PriceCurrency = "EUR"
                            }
                        },
                TrialAccessUrl = "",
                DefaultAccessUrl = "https://nectar.digitaal.noordhoff.nl/",
                ShortDescription = name,
                LongDescription = name,
                Media = new ProductMedia()
                {
                    MainThumbnailUrl = new Media()
                    {
                        Url = imageUrl,
                    },
                },
                RelatedProducts = new List<string>() { },
                BundledProducts = new List<string>() { }
            };
        }



        public virtual Product GetProductEan(string ean)
        {
            var result = GetAllProducts.Where(p => p.Ean.Equals(ean)).ToList();

            return result.FirstOrDefault();
        }


        public virtual List<Product> GetProducts(string subjectCode, string level, int? start, int? limit, string status)
        {
            var result = GetAllProducts;

            return result.ToList();
        }
    }
}
