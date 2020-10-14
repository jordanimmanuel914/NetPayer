﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MultiSafepay
{
    /// <summary>
    /// Generates urls for the different operations available on the MultiSafepay API
    /// </summary>
    internal class UrlProvider
    {
        private readonly string _baseUrl;
        private string _langaugeCode;

        internal UrlProvider(string baseUrl)
        {
            if (!baseUrl.EndsWith("/"))
            {
                _baseUrl = baseUrl + "/";
            }
            else
            {
                _baseUrl = baseUrl;
            }
        }

        internal UrlProvider(string baseUrl, string languageCode)
            : this(baseUrl)
        {
            _langaugeCode = languageCode;
        }


        public string GatewaysUrl(string countryCode = null, string currency = null, int? amount = null)
        {
            var queryStringComponents = new Dictionary<string, string>()
            {
                {"country", WebUtility.UrlEncode(countryCode)},
                {"currency", WebUtility.UrlEncode(currency)},
                {"amount", amount.HasValue ? WebUtility.UrlEncode(amount.ToString()) : null}
            }
                .Where(x => !String.IsNullOrEmpty(x.Value));

            var queryString = String.Join("&", queryStringComponents.Select(x => String.Format("{0}={1}", x.Key, x.Value)));


            if (!String.IsNullOrEmpty(queryString))
            {
                return FormatLanguage(_baseUrl + "gateways?" + queryString, _langaugeCode);
            }

            return FormatLanguage(_baseUrl + "gateways", _langaugeCode);
        }

        public string GatewayUrl(string gatewayName)
        {
            return FormatLanguage(_baseUrl + "gateways/" + gatewayName, _langaugeCode);
        }

        public string IssuersUrl(string gatewayName)
        {
            return FormatLanguage(_baseUrl + "issuers/" + gatewayName, _langaugeCode);
        }

        public string OrderUrl(string orderId)
        {
            return FormatLanguage(_baseUrl + "orders/" + orderId, _langaugeCode);
        }

        public string TransactionUrl(string transactionId)
        {
            return FormatLanguage(_baseUrl + "transactions/" + transactionId, _langaugeCode);
        }

        public string PaymentLinkUrl(string orderId)
        {
            return FormatLanguage(_baseUrl + "orders/" + orderId + "/paymentlink", _langaugeCode);
        }

        public string OrdersUrl()
        {
            return FormatLanguage(_baseUrl + "orders", _langaugeCode);
        }

        public string OrderRefundsUrl(string orderId)
        {
            return FormatLanguage(_baseUrl + "orders/" + orderId + "/refunds", _langaugeCode);
        }

        public string OrderCaptureUrl(string orderId)
        {
            return FormatLanguage(_baseUrl + "orders/" + orderId + "/capture", _langaugeCode);
        }

        public string OrderVoidUrl(string orderId)
        {
            return FormatLanguage(_baseUrl + "capture/" + orderId, _langaugeCode);
        }

        private string FormatLanguage(string baseUrl, string langaugeCode)
        {
            if (String.IsNullOrEmpty(langaugeCode))
            {
                return baseUrl;
            }

            if (baseUrl.Contains("?"))
            {
                return baseUrl + "&locale=" + langaugeCode.ToLower();
            }
            return baseUrl + "?locale=" + langaugeCode.ToLower();
        }
    }
}
