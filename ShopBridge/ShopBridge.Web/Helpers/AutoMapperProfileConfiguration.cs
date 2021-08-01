﻿using ShopBridge.Core.Domain.Catalog;
using ShopBridge.Core.Domain.Messages;
using ShopBridge.Core.Domain.Sale;
using ShopBridge.Core.Domain.User;
using ShopBridge.Web.Areas.Admin.Models.Catalog;
using ShopBridge.Web.Areas.Admin.Models.Sale;
using ShopBridge.Web.Areas.Admin.Models.Support;
using ShopBridge.Web.Models;
using ShopBridge.Web.Models.ManageViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Web.Helpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            // billing address mappings
            CreateMap<BillingAddress, BillingAddressModel>()
                .ReverseMap();
            CreateMap<BillingAddress, CheckoutModel>()
                .ReverseMap();

            // category mappings
            CreateMap<Category, CategoryListModel>();
            CreateMap<Category, CategoryCreateOrUpdateModel>()
                .ReverseMap();

            // manufacturer mappings
            CreateMap<Manufacturer, ManufacturerListModel>();
            CreateMap<Manufacturer, ManufacturerCreateOrUpdateModel>()
                .ReverseMap();

            // order mapping
            CreateMap<OrderManageModel, Order>();

            // product mappings
            CreateMap<Product, ProductListModel>();
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Manufacturers, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());
            CreateMap<Product, ProductCreateOrUpdateModel>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());
            CreateMap<ProductCreateOrUpdateModel, Product>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Specifications, opt => opt.Ignore());

            // review
            CreateMap<Review, ReviewModel>()
                .ReverseMap();

            // specifications
            CreateMap<Specification, SpecificationListModel>();
            CreateMap<Specification, SpecificationCreateOrUpdateModel>()
                .ReverseMap();

            // suport
            CreateMap<ContactUsMessage, ContactUsMessageModel>();
        }
    }
}
