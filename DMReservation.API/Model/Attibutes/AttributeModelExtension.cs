using DMReservation.Domain.DTOs;
using DMReservation.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DMReservation.API.Model.Attibutes
{
    public static class AttributeModelExtension
    {

        public static List<ItemErroDto> GetErrors(this ModelStateDictionary model)
        {
            IEnumerable<ItemErroDto> items = from ms in model
                                             where ms.Value.Errors.Any()
                                             let fieldKey = ms.Key
                                             let errors = ms.Value.Errors
                                             from error in errors
                                             select new ItemErroDto(fieldKey, error.ErrorMessage);

            return items.ToList();
        }

        public static void ValidateModel(this ModelStateDictionary model)
        {
            if (!model.IsValid)
            {
                throw new ApplicationBaseException("The fields in model are required.", "The fields in model are required.", "MDLST", 400, model.GetErrors());
            }
        }
    }
}
