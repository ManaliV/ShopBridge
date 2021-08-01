﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Core.Interface.Services.Messages;
using AutoMapper;
using ShopBridge.Core.Domain.Messages;
using ShopBridge.Web.Areas.Admin.Models.Support;

namespace ShopBridge.Web.Areas.Admin.Controllers
{
    public class ContactUsMessageController : AdminController
    {
        #region Fields

        private readonly IContactUsService _contactUsService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ContactUsMessageController(
            IContactUsService contactUsService,
            IMapper mapper)
        {
            _contactUsService = contactUsService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var entities = _contactUsService.GetAllMessages();
            var messages = new List<ContactUsMessageModel>();

            foreach (var entity in entities)
            {
                messages.Add(_mapper.Map<ContactUsMessage, ContactUsMessageModel>(entity));
            }
            
            return View(messages);
        }

        public IActionResult Message(Guid id)
        {
            if(id != null)
            {
                var messageEntity = _contactUsService.GetMessageById(id);
                if(messageEntity != null)
                {
                    var model = _mapper.Map<ContactUsMessage, ContactUsMessageModel>(messageEntity);

                    if (model.Read == false)
                        _contactUsService.MarkAsRead(id);

                    return View(model);
                }
            }
            
            return RedirectToAction("List");
        }

        // Post: /ContactUsMessage/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0)
                return RedirectToAction("List");

            _contactUsService.DeleteMessages(ids);

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Send(string To, string Subject, string Reply)
        {
            return RedirectToAction("List");
        }

        
        #endregion
    }
}
