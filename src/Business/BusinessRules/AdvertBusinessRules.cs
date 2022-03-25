﻿using Business.Messages.BusinessRulesMessages;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;
using Business.BusinessRules.BannedKeyword;
namespace Business.BusinessRules
{
    internal class AdvertBusinessRules
    {

        internal static IResult CheckFilesSize(IFormFile[] files)
        {
            if (files == null)
            {
                return new ErrorResult("Files is empty");
            }
            if (files.Length == 0)
            {
                return new ErrorResult(AdvertBusinessRulesMessages.GreaterFileSizeMessage);
            }

            if (files.Length > 5)
            {
                return new ErrorResult(AdvertBusinessRulesMessages.LessFileSizeMessage);
            }

            return new SuccessResult();
        }


        internal static IResult CheckDescriptionIllegalKeyword(string description)
        {
          /*  foreach (var keyword in BannedKeyword.BannedKeyword.SearchedKeyword)
            {
                if (description.Contains(keyword))
                {
                    return new ErrorResult($"{AdvertBusinessRulesMessages.BannedKeywordMessage}{BannedKeyword.BannedKeyword.SearchedKeyword}");
                }
            }*/
            return new SuccessResult();
        }
    }
}
