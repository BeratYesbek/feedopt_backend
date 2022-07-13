using System.Linq;
using Business.Messages.BusinessRulesMessages;
using Core.Utilities.Result.Concretes;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Result.Abstracts.IResult;
using Business.BusinessRules.BannedKeyword;
using Core.Extensions;
using Core.Utilities.Algorithms.SearchAlgorithm;
using Core.Entity.Concretes;

namespace Business.BusinessRules
{
    internal static class AdvertBusinessRules
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

        internal static IResult ImageCountForUpdate(int? currentImageCount = 0, int? deletedImages = 0, int? newImagesCount = 0)
        {
            currentImageCount = currentImageCount ?? 0;
            deletedImages = deletedImages ?? 0;
            newImagesCount = newImagesCount ?? 0;
            var imageCount = currentImageCount - deletedImages;
            if (imageCount <= 0 && newImagesCount <= 0)
            {
                return new ErrorResult(AdvertBusinessRulesMessages.DeletedImagesMessage);
            }
            else if ((imageCount + newImagesCount) > 5)
            {
                return new ErrorResult(AdvertBusinessRulesMessages.MaxImageSizeMessage);
            }

            return new SuccessResult();
        }
        internal static IResult EmailConfirmedForCreateAdvert()
        {
           // if (CurrentUser.User.EmailConfirmed)
                return new SuccessResult();

            //return new ErrorResult("Email is not confirmed");
        }


        internal static IResult CheckDescriptionIllegalKeyword(string description)
        {
            var ahoCorasick = new AhoCorasick.AhoCorasick();
            ahoCorasick.SetKeywords(BannedKeyword.BannedKeyword.SearchedKeyword);
            ahoCorasick.Build();
            foreach (var word in ahoCorasick.Find(description))
            {
                if (word is not null)
                {
                    return new ErrorResult($"{AdvertBusinessRulesMessages.BannedKeywordMessage} {string.Join(", ", BannedKeyword.BannedKeyword.SearchedKeyword)}");
                }
            }

            return new SuccessResult();
        }
    }
}
