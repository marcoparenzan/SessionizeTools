using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SlideComposerApp
{
    public static class Slide
    {
        [FunctionName("Slide")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            var query = req.RequestUri.ParseQueryString();
            var idx = int.Parse(query["idx"]);

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://sessionize.com/api/v2/yhpzu19c/view/All");

            var data = JsonSerializer.Deserialize<SessionizeData>(json);

            FontCollection collection = new FontCollection();
            // https://fonts.google.com/specimen/Roboto
            var robotoLightFamily = collection.Install("assets/fonts/Roboto/Roboto-Light.ttf");
            var titleFont = robotoLightFamily.CreateFont(64, FontStyle.Regular);
            var speakerNameFont = robotoLightFamily.CreateFont(36, FontStyle.Regular);

            var sessionData = data.Sessions[idx];
            var speakerData = data.Speakers.FirstOrDefault(xx => xx.Id == sessionData.Speakers[0]);

            var bannerPictureData = await File.ReadAllBytesAsync("assets/images/banner-tile.png");
            var bannerPicture = Image.Load(bannerPictureData);

            var speakerPictureData = await httpClient.GetByteArrayAsync(speakerData.ProfilePicture);
            var speakerPicture = Image.Load(speakerPictureData);
            speakerPicture.Mutate(xx => xx.Resize(360, 360));

            var ms = new MemoryStream();

            var color = Color.FromRgb(0x47, 0x93, 0xe0);

            using (var image = new Image<Rgba32>(1920, 1080))
            {
                image.Mutate(x => x
                    .DrawImage(bannerPicture, new Point(0, 0), 1)
                    .DrawText(new TextGraphicsOptions(
                        new GraphicsOptions
                        {

                        },
                        new TextOptions
                        {
                            WrapTextWidth = 1200,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    ), sessionData.Title.Trim('-', ' '), titleFont, color, new PointF(570, 588))
                    .DrawImage(speakerPicture, new Point(85, 432), 1)
                    .DrawText(new TextGraphicsOptions(
                        new GraphicsOptions
                        {

                        },
                        new TextOptions
                        {
                            HorizontalAlignment = HorizontalAlignment.Center
                        }
                    ), speakerData.FullName, speakerNameFont, color, new PointF(260, 800))
                );
                await image.SaveAsPngAsync(ms);
            }

            var result = new FileContentResult(ms.ToArray(), "image/png");
            result.FileDownloadName = $"datasat0001-{idx}-{speakerData.FullName}.png";
            return result;
        }
    }
}
