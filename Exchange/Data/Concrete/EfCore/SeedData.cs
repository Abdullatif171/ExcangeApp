using Exchange.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Exchange.Data.Concrete.EfCore
{
    public static class SeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin123";
        public static async void TestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateAsyncScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if (context != null)
            {

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<Users>>();

                var user = await userManager.FindByNameAsync(adminUser);

                if (user == null)
                {
                    user = new Users
                    {
                        Name = "Abdullatif",
                        Surname = "Alpaslan",
                        Image = "user.jpeg",
                        UserName = adminUser,
                        Email = "abdullatifalpaslan@outlook.com",
                        PhoneNumber = "5455430947"
                        
                    };

                    await userManager.CreateAsync(user, adminPassword);
                }

                if (!context.MainCategries.Any())
                {
                    context.MainCategries.AddRange(
                        new MainCategory { 
                            Text = "Kadın",
                            Url = "Kadin",
                            Categories = new List<Category>{
                                new Category { Text = "Giyim", Url = "Giyim",
                                    Tags = new List<Tag>{
                                        new Tag { Text = "Elbise", Url = "Elbise"},
                                        new Tag { Text = "Tişört", Url = "Tisrot"},
                                        new Tag { Text = "Gömlek", Url = "Gomlek"},
                                        new Tag { Text = "Mont", Url = "Mont" }
                                    }
                                }
                            }
                        },
                        new MainCategory {
                            Text = "Erkek",
                            Url = "Erkek",
                            Categories = new List<Category>{
                                new Category { Text = "Giyim", Url = "Giyim",
                                    Tags = new List<Tag>{
                                        new Tag { Text = "Tişört", Url = "Tisrot"},
                                        new Tag { Text = "Şort", Url = "Sort"},
                                        new Tag { Text = "Gömlek", Url = "Gomlek"},
                                        new Tag { Text = "Mont", Url = "Mont" }
                                    }
                                }
                            }
                        },
                        new MainCategory {
                            Text = "Anne & Çocuk",
                            Url = "AnneAndCocuk",
                            Categories = new List<Category>{
                                new Category { Text = "Bebek", Url = "Bebek",
                                    Tags = new List<Tag>{
                                        new Tag { Text = "Tişört", Url = "Tisrot"},
                                        new Tag { Text = "Şort", Url = "Sort"},
                                        new Tag { Text = "Elbise", Url = "Elbise"},
                                        new Tag { Text = "Tulum", Url = "Tulum" }
                                    }
                                }
                            }
                        },
                        new MainCategory {
                            Text = "Ev & Mobilya",
                            Url = "EvAndMobilya",
                            Categories = new List<Category>{
                                new Category { Text = "Mobilya", Url = "Mobilya",
                                    Tags = new List<Tag>{
                                        new Tag { Text = "Dolap", Url = "Dolap"},
                                        new Tag { Text = "Zigon", Url = "Zigon"},
                                        new Tag { Text = "Gardırop", Url = "Gardirop"},
                                        new Tag { Text = "Sandalye", Url = "Sandalye" }
                                    }
                                }
                            }
                            },
                        new MainCategory {
                            Text = "Süpermarket",
                            Url = "Supermarket",
                            },
                        new MainCategory {
                            Text = "Kozmetik",
                            Url = "Kozmetik",
                            },
                        new MainCategory {
                            Text = "Ayakkabı & Çanta",
                            Url = "AyakkabiAndCanta",
                            },
                        new MainCategory{
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
                        new MainCategory {
                            Text = "Spor & Outdoor",
                            Url = "SporAndOutdoor",
                            Categories = new List<Category>{
                                new Category { Text = "Spor Üst Giyim", Url = "SporUstGiyim",
                                    Tags = new List<Tag>{
                                        new Tag { Text = "Atlet", Url = "Atlet"},
                                        new Tag { Text = "Forma", Url = "Forma"},
                                        new Tag { Text = "SweatShirt", Url = "SweatShirt"},
                                        new Tag { Text = "Spor Mont", Url = "SporMont" }
                                    }
                                }
                            }
                            }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new Users
                        {
                            Name = "Abdullatif",
                            Surname = "Alpaslan",
                            Image = "user.jpeg",
                            UserName = adminUser,
                            Email = "info@latif.com",
                            PhoneNumber = "5455430947",
                            Product = new List<Products>{
                                new Products {
                                    Title = "Huawei MatebookB 512GB SSD Windows 11 Home 16'IPS Taşınabilir Bilgisayar",
                                    Description = "Cihaz Ağırlığı: 1 - 2 kg, Ram (Sistem Belleği): 16 GB, Bluetooth Özelliği: Var, Optik Sürücü: Yok, Ekran Kartı Hafızası: Paylaşımlı, Dokunmatik Ekran: Yok, SSD Kapasitesi: 512 GB, HDMI: Var, Ekrk",
                                    Url = "HuaweiMatebook",
                                    ExchangeState = false,
                                    SellState = true,
                                    Tags = context.Tags.Take(3).ToList(),
                                    Image = "HuaweiMatebook.jpg",
                                    Price = 21.999m,
                                }
                            }
                            
                        });
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
