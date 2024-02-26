using Exchange.Entity;
using Microsoft.EntityFrameworkCore;
namespace Exchange.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void TestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateAsyncScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }



                if (!context.MainCategries.Any())
                {
                    context.MainCategries.AddRange(
                        new MainCategory { Text = "Kadın", Url = "Kadin" },
                        new MainCategory { Text = "Erkek", Url = "Erkek" },
                        new MainCategory { Text = "Anne & Çocuk", Url = "AnneAndCocuk" },
                        new MainCategory { Text = "Ev & Mobilya", Url = "EvAndMobilya" },
                        new MainCategory { Text = "Süpermarket", Url = "Supermarket" },
                        new MainCategory { Text = "Kozmetik", Url = "Kozmetik" },
                        new MainCategory { Text = "Ayakkabı & Çanta", Url = "AyakkabiAndCanta" },
                        new MainCategory
                        {
                            Text = "Elektrnik",
                            Url = "Elektrnik",
                            Categories = new List<Category>{
                                new Category { Text = "Bilgisayar & Tablet", Url = "BilgisayarAndTablet",
                                    Tags = new List<Tag>{
                                        new Tag { Text = "Bilgisayar", Url = "Bilgisayar"},
                                        new Tag { Text = "Tablet", Url = "Tablet"},
                                        new Tag { Text = "Yazıcı", Url = "Yazici"},
                                        new Tag { Text = "Klavye", Url = "Klavye" }
                        }
                            }
                            
                        }
                        },
                        new MainCategory { Text = "Spor & Outdoor", Url = "SporAndOutdoor" }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Users { UserName = "Latif", Image="user.jpeg", Name = "Abdllatif", Surname = "Alpaslan", Email = "info@latifa.com", }
                    );
                    context.SaveChanges();
                }

                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Products
                        {
                            Title = "Huawei Matebook D16 2024 Intel Core i5 12450H 16GB 512GB SSD Windows 11 Home 16'IPS Taşınabilir Bilgisayar",
                            Description = "Cihaz Ağırlığı: 1 - 2 kg, Ram (Sistem Belleği): 16 GB, Bluetooth Özelliği: Var, Optik Sürücü: Yok, Ekran Kartı Hafızası: Paylaşımlı, Dokunmatik Ekran: Yok, SSD Kapasitesi: 512 GB, HDMI: Var, Ekran Kartı Tipi: Dahili Ekran Kartı, eMMC Kapasitesi: Yok, Max Ekran Çözünürlüğü: 1920 x 1200, Bellek Hızı: Belirtilmemiş,Renk: Gri, İşletim Sistemi: Windows 11 Home, Ekran Kartı Bellek Tipi: Onboard, Maksimum İşlemci Hızı: 4,4 GHz, Ram Tipi: Belirtilmemiş, Webcam: Var, Harddisk Kapasitesi: Yok, İşlemci Cache: 12 MB Cache, Parmak İzi Okuyucu: Var, Ürün Modeli: Notebook, Ekran Boyutu: 16 inç, Ekran Kartı: Paylaşımlı, Kart Okuyucu: Yok, Klavye: Q Türkçe, İşlemci Nesli: 12.Nesil, İşlemci: 12450H, Ekran Panel Tipi: IPS, İşlemci Tipi: Intel Core i5, Diğer: Garanti Süresi (Ay): 24, Yurt Dışı Satış: Yok",
                            Url = "HuaweiMatebook",
                            ExchangeState = false,
                            SellState = true,
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "HuaweiMatebook.jpg",
                            Price = 21.999m,
                            Comment = new List<Comment> {
                                new Comment { Text = "modeller cok değişik değişik. Alanlar dikkat etmeli. 12 . nesil işlemci bu. klavyede aydınlatma yok çok üzücü bu durum. Daha ayrıntılı belirtilebilirdi. 13. nesillerde varmış bu özellik. Bataryada da düşüklük var ama onun için bir eksiklik hissetmediğim için yorum yok. Ama şıklığı ve ekran kalitesi ,açılış hızı, parmak iziyle hızlıca geçiş vs harika.", PublishedOn = DateTime.Now.AddDays(-20), UserId = 1},
                                new Comment { Text = "Çok araştırarak aldım. Bu özelliklerde bu işlemcili bilgisayarların hemen hemen hepsi 27-30 bin seviyelerinde. Huawei yapmış tasarım harika, ağır değil hafif. ekran kasa oranı çok başarılı.", PublishedOn = DateTime.Now.AddDays(-10), UserId = 1}
                            }
                        },
                        new Products
                        {
                            Title = "Huawei Matebook D16 2024 Intel Core i5 12450H 16GB 512GB SSD Windows 11 Home 16'IPS Taşınabilir Bilgisayar",
                            Description = "Cihaz Ağırlığı: 1 - 2 kg, Ram (Sistem Belleği): 16 GB, Bluetooth Özelliği: Var, Optik Sürücü: Yok, Ekran Kartı Hafızası: Paylaşımlı, Dokunmatik Ekran: Yok, SSD Kapasitesi: 512 GB, HDMI: Var, Ekran Kartı Tipi: Dahili Ekran Kartı, eMMC Kapasitesi: Yok, Max Ekran Çözünürlüğü: 1920 x 1200, Bellek Hızı: Belirtilmemiş,Renk: Gri, İşletim Sistemi: Windows 11 Home, Ekran Kartı Bellek Tipi: Onboard, Maksimum İşlemci Hızı: 4,4 GHz, Ram Tipi: Belirtilmemiş, Webcam: Var, Harddisk Kapasitesi: Yok, İşlemci Cache: 12 MB Cache, Parmak İzi Okuyucu: Var, Ürün Modeli: Notebook, Ekran Boyutu: 16 inç, Ekran Kartı: Paylaşımlı, Kart Okuyucu: Yok, Klavye: Q Türkçe, İşlemci Nesli: 12.Nesil, İşlemci: 12450H, Ekran Panel Tipi: IPS, İşlemci Tipi: Intel Core i5, Diğer: Garanti Süresi (Ay): 24, Yurt Dışı Satış: Yok",
                            Url = "HuaweiMatebook",
                            ExchangeState = true,
                            SellState = false,
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "HuaweiMatebook.jpg",
                            Price = 21.999m,
                            Comment = new List<Comment> {
                                new Comment { Text = "modeller cok değişik değişik. Alanlar dikkat etmeli. 12 . nesil işlemci bu. klavyede aydınlatma yok çok üzücü bu durum. Daha ayrıntılı belirtilebilirdi. 13. nesillerde varmış bu özellik. Bataryada da düşüklük var ama onun için bir eksiklik hissetmediğim için yorum yok. Ama şıklığı ve ekran kalitesi ,açılış hızı, parmak iziyle hızlıca geçiş vs harika.", PublishedOn = DateTime.Now.AddDays(-20), UserId = 1},
                                new Comment { Text = "Çok araştırarak aldım. Bu özelliklerde bu işlemcili bilgisayarların hemen hemen hepsi 27-30 bin seviyelerinde. Huawei yapmış tasarım harika, ağır değil hafif. ekran kasa oranı çok başarılı.", PublishedOn = DateTime.Now.AddDays(-25), UserId = 1},
                                new Comment { Text = "Çok araştırarak aldım. Bu özelliklerde bu işlemcili bilgisayarların hemen hemen hepsi 27-30 bin seviyelerinde. Huawei yapmış tasarım harika, ağır değil hafif. ekran kasa oranı çok başarılı.", PublishedOn = DateTime.Now.AddDays(-30), UserId = 1}
                            }
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}

