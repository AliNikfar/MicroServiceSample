using AutoMapper;
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
    }
}
