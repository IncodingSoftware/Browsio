namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Linq;
    using Browsio.Amazon;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(AmazonService))]
    public class When_amazon_service
    {
        It should_be_fetch_mustang = () => new AmazonService()
                                                   .Fetch("Mustang", 1, "VideoGames")
                                                   .Should(list =>
                                                               {
                                                                   list.Count.ShouldEqual(10);
                                                                   list.First(r => r.ASIN == "B003RRTXYW")
                                                                       .ShouldEqualWeak(new AmazonItem
                                                                                            {
                                                                                                    Title = "Rock Band 3 Wireless Fender Mustang PRO-Guitar Controller for Xbox 360", 
                                                                                                    Description = "All-new Wireless Fender Mustang PRO-Guitar Controller for Rock Band 3.Plays Rock Band 3 Guitar and Bass parts / Play real chords and melodies with new Rock Band Pro mode.17-fret touch-sensitive neck with 6 buttons per fret provides 102 active finger positions / 6 low-latency strings for authentic note strumming.Advanced tilt sensor for Overdrive activation.Use as MIDI Guitar Controller when not playing Rock Band (compatible with most MIDI sequencers).", 
                                                                                                    Price = 59.99, 
                                                                                                    ASIN = "B003RRTXYW", 
                                                                                                    Author = "Mad Catz", 
                                                                                                    Image = "http://ecx.images-amazon.com/images/I/51qrQEDqfTL._SL160_.jpg", 
                                                                                            });
                                                               });

        It should_be_fetch_mustang_page_2 = () => new AmazonService()
                                                          .Fetch("Mustang", 2, "VideoGames")
                                                          .Should(list =>
                                                                      {
                                                                          list.Count.ShouldEqual(10);
                                                                          list.First(r => r.ASIN == "B00525AJAE")
                                                                              .ShouldEqualWeak(new AmazonItem
                                                                                                   {
                                                                                                           Title = "Nintendo DSi XL Skin Decal Sticker - Mustang Horse",
                                                                                                           Description = "This 4-piece sticker skin set Cover Front, Back and Inside Panels,follow the easy peel and stick instruction to cover and protect your DS.Ultra-high resolution, brilliant full-color design that are processed with UV resistant inks on self-adhesive vinyl.It is coated with an extra layer of water-proof, smooth high-glossy protective film for the ultimate durability.Easy Apply and Leave no residue when remove, Eye Poping Designs, guarantees NO FADING!.For DSi Only, NOT For DS Lite Use.", 
                                                                                                           Price = 0,
                                                                                                           ASIN = "B00525AJAE", 
                                                                                                           Author = string.Empty, 
                                                                                                           Image = "http://ecx.images-amazon.com/images/I/41qOsr1IhRL._SL160_.jpg", 
                                                                                                   });
                                                                      });

        It should_be_fetch_terminator = () => new AmazonService()
                                                      .Fetch("terminator", 1, "DVD")
                                                      .Should(list =>
                                                                  {
                                                                      list.Count.ShouldEqual(10);
                                                                      list
                                                                              .First(r => r.ASIN == "B0024Y7H10")
                                                                              .ShouldEqualWeak(new AmazonItem
                                                                                                   {
                                                                                                           Title = "Pilot [HD]", 
                                                                                                           Description = string.Empty, 
                                                                                                           Price = 0, 
                                                                                                           ASIN = "B0024Y7H10", 
                                                                                                           Author = "Lena Headey,Thomas Dekker,Summer Glau,Richard T. Jones,Owain Yeoman,", 
                                                                                                           Image = "http://ecx.images-amazon.com/images/I/51rjIe9yMAL._SL160_.jpg", 
                                                                                                   });
                                                                  });
    }
}