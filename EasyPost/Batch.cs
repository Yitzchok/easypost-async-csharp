/*
 * Licensed under The MIT License (MIT)
 * 
 * Copyright (c) 2014 EasyPost
 * Copyright (C) 2017 AMain.com, Inc.
 * All Rights Reserved
 */

using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace EasyPost
{
    /// <summary>
    /// EasyPost Batch object
    /// </summary>
    public class Batch : EasyPostObject
    {
        /// <summary>
        /// The overall state. Possible values are "creating", "creation_failed", "created", "purchasing", "purchase_failed", "purchased", "label_generating", and "label_generated"
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The number of shipments added
        /// </summary>
        public int NumShipments { get; set; }

        /// <summary>
        /// List of batch shipments
        /// </summary>
        public List<BatchShipment> Shipments { get; set; }

        /// <summary>
        /// A map of BatchShipment statuses to the count of BatchShipments with that status. Valid statuses are "postage_purchased", "postage_purchase_failed", "queued_for_purchase", and "creation_failed"
        /// </summary>
        public Dictionary<string, int> Status { get; set; }

        /// <summary>
        /// The label image url
        /// </summary>
        public string LabelUrl { get; set; }

        /// <summary>
        /// The created ScanForm
        /// </summary>
        public ScanForm ScanForm { get; set; }

        /// <summary>
        /// The created Pickup
        /// </summary>
        public Pickup Pickup { get; set; }
    }

    /// <summary>
    /// Batch API implementation
    /// </summary>
    public partial class EasyPostClient
    {

        /// <summary>
        /// Retrieve a Batch from its id.
        /// </summary>
        /// <param name="id">String representing a Batch. Starts with "batch_".</param>
        /// <returns>Batch instance.</returns>
        public Batch GetBatch(
            string id)
        {
            var request = new EasyPostRequest("batches/{id}");
            request.AddUrlSegment("id", id);

            return Execute<Batch>(request);
        }

        /// <summary>
        /// Create a Batch.
        /// </summary>
        /// <param name="shipments">Optional list of shipments</param>
        /// <param name="reference">Optional reference</param>
        /// <returns>EasyPost.Batch instance.</returns>
        public Batch CreateBatch(
            IEnumerable<Shipment> shipments = null, 
            string reference = null)
        {
            var request = new EasyPostRequest("batches", Method.POST);
            var parameters = new Dictionary<string, object>();
            if (reference != null) {
                parameters.Add("reference", reference);
            }
            if (shipments != null) {
                parameters.Add("shipments", shipments.Select(shipment => shipment.AsDictionary()).ToList());
            }
            request.AddBody(parameters, "batch");

            return Execute<Batch>(request);
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="id">Batch id to add the shipments to</param>
        /// <param name="shipmentIds">List of shipment ids to be added.</param>
        /// <returns>Batch instance.</returns>
        public Batch AddShipmentsToBatch(
            string id,
            IEnumerable<string> shipmentIds)
        {
            var request = new EasyPostRequest("batches/{id}/add_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            var body = shipmentIds.Select(shipmentId => new Dictionary<string, object> { { "id", shipmentId } }).ToList();
            request.AddBody(body, "shipments");

            return Execute<Batch>(request);
        }

        /// <summary>
        /// Add shipments to the batch.
        /// </summary>
        /// <param name="id">Batch id to add the shipments to</param>
        /// <param name="shipments">List of Shipment objects to be added.</param>
        /// <returns>Batch instance.</returns>
        public Batch AddShipmentsToBatch(
            string id,
            IEnumerable<Shipment> shipments)
        {
            return AddShipmentsToBatch(id, shipments.Select(shipment => shipment.Id));
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="id">Batch id to add the shipments to</param>
        /// <param name="shipmentIds">List of shipment ids to be removed.</param>
        /// <returns>Batch instance.</returns>
        public Batch RemoveShipmentsFromBatch(
            string id,
            IEnumerable<string> shipmentIds)
        {
            var request = new EasyPostRequest("batches/{id}/remove_shipments", Method.POST);
            request.AddUrlSegment("id", id);

            var body = shipmentIds.Select(shipmentId => new Dictionary<string, object> { { "id", shipmentId } }).ToList();
            request.AddBody(body, "shipments");

            return Execute<Batch>(request);
        }

        /// <summary>
        /// Remove shipments from the batch.
        /// </summary>
        /// <param name="id">Batch id to add the shipments to</param>
        /// <param name="shipments">List of Shipment objects to be removed.</param>
        /// <returns>Batch instance.</returns>
        public Batch RemoveShipmentsFromBatch(
            string id,
            IEnumerable<Shipment> shipments)
        {
            return RemoveShipmentsFromBatch(id, shipments.Select(shipment => shipment.Id));
        }

        /// <summary>
        /// Buy labels for all shipments within a batch. The Batch's state must be "created" before purchasing.
        /// </summary>
        /// <param name="id">Batch id to add the shipments to</param>
        /// <returns>Batch instance.</returns>
        public Batch BuyLabelsForBatch(
            string id)
        {
            var request = new EasyPostRequest("batches/{id}/buy", Method.POST);
            request.AddUrlSegment("id", id);

            return Execute<Batch>(request);
        }

        /// <summary>
        /// Asynchronously generate a label containing all of the Shipment labels belonging to the batch.
        /// </summary>
        /// <param name="id">Batch id to generate the label for</param>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <param name="orderBy">Optional parameter to order the generated label. Ex: "reference DESC"</param>
        /// <returns>Batch instance.</returns>
        public Batch GenerateLabelForBatch(
            string id,
            string fileFormat,
            string orderBy = null)
        {
            var request = new EasyPostRequest("batches/{id}/label", Method.POST);
            request.AddUrlSegment("id", id);

            var body = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("file_format", fileFormat)
            };
            if (orderBy != null) {
                body.Add(new KeyValuePair<string, string>("order_by", orderBy));
            }
            request.AddBody(body);

            return Execute<Batch>(request);
        }

        /// <summary>
        /// Asychronously generate a scan form for the batch.
        /// </summary>
        /// <param name="id">Batch id to generate the label for</param>
        /// <returns>Batch instance.</returns>
        public Batch GenerateScanFormForBatch(
            string id)
        {
            var request = new EasyPostRequest("batches/{id}/scan_form", Method.POST);
            request.AddUrlSegment("id", id);

            return Execute<Batch>(request);
        }
    }
}