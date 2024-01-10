using Discount.Grpc.Protos;

namespace Basket.Api.GrpsServices
{
    public class DiscountGrpcService
    {
        //I'm Sure it's not true and makes coupling
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            this._discountProtoService = discountProtoService;
        }


        public async Task<CouponeModel> GetDiscount(string productName)
        {

            var discountRequest = new GetDiscountRequest { ProductName = productName };
           return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
