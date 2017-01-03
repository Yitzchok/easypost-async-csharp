/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using RestSharp;

namespace EasyPost
{
    public class Container : EasyPostObject
    {
        /// <summary>
        /// Name of the container
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the container
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Length of the container
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Width of the container
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of the container
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Max weight of the container
        /// </summary>
        public double MaxWeight { get; set; }
    }

    /// <summary>
    /// Container API implementation
    /// </summary>
    public partial class EasyPostClient
    {
        /// <summary>
        /// Retrieve a Container from its id or reference.
        /// </summary>
        /// <param name="id">String representing a Container. Starts with "container_" if passing an id.</param>
        /// <returns>Container instance.</returns>
        public Container GetContainer(
            string id)
        {
            var request = new EasyPostRequest("containers/{id}");
            request.AddUrlSegment("id", id);

            return Execute<Container>(request);
        }

        /// <summary>
        /// Create a Container.
        /// </summary>
        /// <param name="container">Container parameters</param>
        /// <returns>EasyPost.Container instance.</returns>
        public Container CreateContainer(
            Container container)
        {
            if (container.Id != null) {
                throw new ResourceAlreadyCreated();
            }

            var request = new EasyPostRequest("containers", Method.POST);
            request.AddBody(container.AsDictionary(), "container");

            return Execute<Container>(request);
        }
    }
}