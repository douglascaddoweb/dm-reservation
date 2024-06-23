using DMReservation.Application.Interfaces.UseCases.DeliveryManUC;
using DMReservation.Domain.Entities;
using DMReservation.Domain.Exceptions;
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

        /// <summary>
        /// Faz o upload da imagem da CNH enviada
        /// </summary>
        /// <param name="file"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task ExecuteAsync(IFormFile file, int id)
        {
            try
            {
                string format = GetFileFormat(file.FileName);

                DeliveryMan delivery = await _deliveryManRepository.FindIdAsync(id);

                if (delivery is not DeliveryMan) throw new ApplicationBaseException(MessageSetting.RegistryNotFound, "UPDE01");

                string fileNewName = $"{Guid.NewGuid()}.{format}";

                byte[] bytesFile = ConvertFileToByteArray(file);

                string directory = CreateFilePath(fileNewName);

                await File.WriteAllBytesAsync(directory, bytesFile);

                if (!VerifyExistsImage(directory)) throw new ApplicationBaseException(MessageSetting.UploadImageError, "UPDE02");

                delivery.SetImage(fileNewName);
                _deliveryManRepository.Update(delivery);
                await _deliveryManRepository.CommitAsync();
            }
            catch (ApplicationBaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationBaseException(ex.Message, MessageSetting.ProcessError, "GNUPDE");
            }
        }

        /// <summary>
        /// Retorna o formato da imagem
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GetFileFormat(string filename)
        {

            string format = filename.Split('.').Last();
            if (!formatAcept.Contains(format)) throw new ApplicationBaseException(MessageSetting.FormatImageInvalid, "UPDE03");

            return format;
        }

        /// <summary>
        /// Converte o arquivo para array de bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private byte[] ConvertFileToByteArray(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Salva a imagem na pasta e diretorios definidos no appsettings
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string CreateFilePath(string filename)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _configuration["Directory:Images"], filename);
        }

        /// <summary>
        /// Verifica se a imagem existe na pasta de imagens
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool VerifyExistsImage(string filename)
        {
            return File.Exists(filename);
        }
    }
}
