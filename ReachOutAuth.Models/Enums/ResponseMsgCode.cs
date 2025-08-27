using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReachOutAuth.Models.Enums
{
    public enum ResponseMsgCode
    {
        InvalidUser,
        InvalidExternalOrderId,
        InvalidShippingMethod,
        InvalidRecipientData,
        InvalidSKU,
        SkuAlreadyExists,
        FatalException,
        DuplicateOrder,
        Success,
        ProductSuccess,
        InvalidTerritoryId,
        ProductUpdateSuccess,
        ProductUpdateSuccessExpired,
        MissingRequiredFields,
        FatalErrorParsingData,
        ProductDoesNotExist,
        ProductMissingMetaData
    }

    public class ResponseMessages
    {
        public ResponseMessages()
        {
            List<MsgCode> messages = new List<MsgCode>();
            MsgCode x = new MsgCode() { Code = "100.10", Message = "Invalid User" }; messages.Add(x);
            x = new MsgCode() { Code = "100.20", Message = "Missing External Order Id" }; messages.Add(x);
            x = new MsgCode() { Code = "100.30", Message = "Fatal error determining shipping method" }; messages.Add(x);
            x = new MsgCode() { Code = "200.10", Message = "Invalid Recipient Data" }; messages.Add(x);
            x = new MsgCode() { Code = "300.10", Message = "Invalid SKU" }; messages.Add(x);
            x = new MsgCode() { Code = "300.20", Message = "Sku alread exists" }; messages.Add(x);
            x = new MsgCode() { Code = "500.10", Message = "Fatal Exception" }; messages.Add(x);
            x = new MsgCode() { Code = "100.40", Message = "Duplicate order received" }; messages.Add(x);
            x = new MsgCode() { Code = "201.00", Message = "Order Created" }; messages.Add(x);
            x = new MsgCode() { Code = "201.00", Message = "Product Created" }; messages.Add(x);
            x = new MsgCode() { Code = "300.10", Message = "Invalid Territory Id" }; messages.Add(x);
            x = new MsgCode() { Code = "201.00", Message = "Product Updated" }; messages.Add(x);
            x = new MsgCode() { Code = "201.00", Message = "Product Updated, transitioned from active to expired" }; messages.Add(x);
            x = new MsgCode() { Code = "100.40", Message = "Product missing required fields" }; messages.Add(x);
            x = new MsgCode() { Code = "100.50", Message = "Fatal Exception parsing CSV file" }; messages.Add(x);
            x = new MsgCode() { Code = "201.10", Message = "Ignored, product does not exist" }; messages.Add(x);
            x = new MsgCode() { Code = "300.30", Message = "Metadata missing for product" }; messages.Add(x);

            Messages = messages;
        }
        public List<MsgCode> Messages { get; set; }
    }

    public class MsgCode
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}