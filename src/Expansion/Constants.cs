// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 01-08-2019
// ***********************************************************************
// <copyright file="Constants.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The string line feed
        /// </summary>
        public const string StrLineFeed = "\n";

        /// <summary>
        /// <form asp-action="Create"></form>
        /// </summary>
        public const string AspActionCreate = @"<form asp-action=""Create"">";  //<form asp-action="Create">

        /// <summary>
        /// <form asp-action="Edit"></form>
        /// </summary>
        public const string AspActionEdit = @"<form asp-action=""Edit"">";      //<form asp-action="Edit">

        /// <summary>
        /// <div class="col-md-4"></div>
        /// </summary>
        public const string AspDivColMd4 = @"<div class=""col-md-4"">";         //<div class="col-md-4">

        /// <summary>
        /// The ASP div end tag
        /// </summary>
        public const string AspDivEndTag = "</div>";                            //</div>

        /// <summary>
        /// <div class="form-group"></div>
        /// </summary>
        public const string AspDivFormGroup = @"<div class=""form-group"">";    //<div class="form-group">

        /// <summary>
        /// <div class="row"></div>
        /// </summary>
        public const string AspDivRow = @"<div class=""row"">";                 //<div class="row">

        /// <summary>
        /// The div start tag - "<div>"</div>
        /// </summary>
        public const string AspDivStartTag = "<div>";                           //</div>

        /// <summary>
        /// The ASP validate model only
        /// </summary>
        public const string AspValidateModelOnly = @"<div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>";

        /// <summary>
        /// The key suffix
        /// </summary>
        public const string KeySuffix = "ID";
        /// <summary>
        /// The many to many
        /// </summary>
        public const string ManyToMany = "M2M";
        /// <summary>
        /// The many to one
        /// </summary>
        public const string ManyToOne = "M21";
        /// <summary>
        /// The one to many
        /// </summary>
        public const string OneToMany = "12M";
        /// <summary>
        /// The one to one
        /// </summary>
        public const string OneToOne = "121";
        /// <summary>
        /// The type size image
        /// </summary>
        public const int TypeSizeImage = 2147483647;

        /// <summary>
        /// The type size ntext
        /// </summary>
        public const int TypeSizeNtext = 1073741823;
        /// <summary>
        /// The type size text
        /// </summary>
        public const int TypeSizeText = 2147483647;

        //public enum Actions
        //{
        //    Get = "GET",
        //    Put = "PUT",
        //    Post = "POST",
        //    ConstructQuery = "QUERY",
        //    Delete = "DELETE"
        //}
    }
}