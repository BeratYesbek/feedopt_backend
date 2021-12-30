using Microsoft.AspNetCore.Http;

namespace Entity.ApiEntity
{
    /// <summary>
    ///  purpose of this class if formData coming from api it will come to be formFile.
    /// we cannot give being parameter non IEntity to business layer .
    /// we have to give IEntity class for best practices.
    /// /// </summary>
    public class MissingDeclarationImageApiEntity
    {
        public string MissingDeclarationImage { get; set; }

        public IFormFile[] FormFiles { get; set; }
    }
}