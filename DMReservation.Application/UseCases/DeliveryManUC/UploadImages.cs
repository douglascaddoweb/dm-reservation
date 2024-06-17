using DMReservation.Application.Interfaces.UseCases.DeliveryManUC;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Interfaces.Infra;
using DMReservation.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace DMReservation.Application.UseCases.DeliveryManUC
{
    public class UploadImages : IUploadImages
    {
        private string[] formatAcept = ["png", "bmp"];
        private readonly IConfiguration _configuration;
        private readonly IDeliveryManRepository _deliveryManRepository;

        public UploadImages(IConfiguration configuration, IDeliveryManRepository deliveryManRepository)
        {
            _configuration = configuration;
            _deliveryManRepository = deliveryManRepository;
        }

        public async Task ExecuteAsync(IFormFile file, int id)
        {
            string format = GetFileFormat(file.FileName);

            DeliveryMan delivery = await _deliveryManRepository.FindIdAsync(id);

            if (delivery is not DeliveryMan) throw new Exception(MessageSetting.RegistryNotFound);

            string fileNewName = $"{Guid.NewGuid()}.{format}";

            byte[] bytesFile = ConvertFileToByteArray(file);

            string directory = CreateFilePath(fileNewName);

            await File.WriteAllBytesAsync(directory, bytesFile);

            if (!VerifyExistsImage(directory)) throw new Exception(MessageSetting.UploadImageError);

            delivery.SetImage(fileNewName);
            _deliveryManRepository.Update(delivery);
            await _deliveryManRepository.CommitAsync();
        }

        public string GetFileFormat(string filename)
        {

            string format = filename.Split('.').Last();
            if (!formatAcept.Contains(format)) throw new Exception(MessageSetting.FormatImageInvalid);

            return format;
        }


        public byte[] ConvertFileToByteArray(IFormFile file)
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public string CreateFilePath(string filename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _configuration["Directory:Images"], filename);
        }

        public bool VerifyExistsImage(string filename)
        {
            return File.Exists(filename);
        }
    }
}
