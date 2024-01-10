using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<CouponeModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupone = await _discountRepository.GetDiscount(request.ProductName);
            if (coupone == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"discount with ProductName = {request.ProductName} is not found"));
            _logger.LogInformation("Discount is Retrive for ProductName");
            return _mapper.Map<CouponeModel>(coupone);
            //return new CouponeModel
            //{
            //    Id = coupone.Id,
            //    Amount = coupone.Amount,
            //    Description = coupone.Description,
            //    ProductName = coupone.ProductName
            //};
        }
        public async override Task<CouponeModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupone>(request.Coupone);
            await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation($"Discount is Successfuly created for product {coupon.ProductName} ");
            return _mapper.Map<CouponeModel>(coupon);
        }
        public async override Task<CouponeModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupone>(request.Coupone);
            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount is Successfuly updated for product {coupon.ProductName} ");
            return  _mapper.Map<CouponeModel>(coupon);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
             var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
            return  new DeleteDiscountResponse
            {
                Success = deleted
            };
            

        }

    }
}
