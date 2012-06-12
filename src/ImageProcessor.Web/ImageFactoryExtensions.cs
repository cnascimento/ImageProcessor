﻿// -----------------------------------------------------------------------
// <copyright file="ImageFactoryExtensions.cs" company="James South">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ImageProcessor.Web
{
    #region Using
    using System.Collections.Generic;
    using System.Linq;
    using ImageProcessor.Processors;
    using ImageProcessor.Web.Config;
    #endregion

    /// <summary>
    /// Extends the ImageFactory class to provide a fluent api.
    /// </summary>
    public static class ImageFactoryExtensions
    {
        /// <summary>
        /// Auto processes image files based on any querystring parameters added to the image path.
        /// </summary>
        /// <param name="factory">
        /// The current instance of the <see cref="T:ImageProcessor.ImageFactory"/> class
        /// that this method extends.
        /// </param>
        /// <returns>
        /// The current instance of the <see cref="T:ImageProcessor.ImageFactory"/> class.
        /// </returns>
        public static ImageFactory AutoProcess(this ImageFactory factory)
        {
            if (factory.ShouldProcess)
            {
                // Get a list of all graphics processors that have parsed and matched the querystring.
                List<IGraphicsProcessor> list =
                    ImageProcessorConfig.Instance.GraphicsProcessors.Where(x => x.MatchRegexIndex(factory.QueryString) != int.MaxValue).OrderBy(
                        y => y.SortOrder).ToList();

                // Loop through and process the image.
                foreach (IGraphicsProcessor graphicsProcessor in list)
                {
                    factory.Image = graphicsProcessor.ProcessImage(factory);
                }
            }

            return factory;
        }
    }
}