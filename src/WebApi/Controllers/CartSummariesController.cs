﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Entity.Concretes;

namespace WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartSummariesController : ControllerBase
    {
        private readonly ICartSummaryService _cartSummaryService;

        public CartSummariesController(ICartSummaryService cartSummariesService)
        {
            _cartSummaryService = cartSummariesService;
        }

        [HttpPost("add")]
        protected IActionResult Add(CartSummary cartSummary)
        {
            var result = _cartSummaryService.Add(cartSummary);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        protected IActionResult Update(CartSummary cartSummary)
        {
            var result = _cartSummaryService.Update(cartSummary);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        protected IActionResult Delete(CartSummary cartSummary)
        {
            var result = _cartSummaryService.Delete(cartSummary);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        protected IActionResult Get(int id)
        {
            var result = _cartSummaryService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getAll")]
        protected IActionResult GetAll()
        {
            var result = _cartSummaryService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}