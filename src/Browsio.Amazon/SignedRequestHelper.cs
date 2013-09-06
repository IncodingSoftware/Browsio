/**********************************************************************************************
 * Copyright 2009 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use this file 
 * except in compliance with the License. A copy of the License is located at
 *
 *       http://aws.amazon.com/apache2.0/
 *
 * or in the "LICENSE.txt" file accompanying this file. This file is distributed on an "AS IS"
 * BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under the License. 
 *
 * ********************************************************************************************
 *
 *  Amazon Product Advertising API
 *  Signed Requests Sample Code
 *
 *  API Version: 2009-03-31
 *
 */
namespace Browsio.Amazon
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Xml;

    #endregion

    internal class SignedRequestHelper
    {
        #region Constants

        const string REQUEST_URI = "/onca/xml";

        const string REQUEST_METHOD = "GET";

        #endregion

        #region Fields

        readonly string endPoint;

        readonly string akid;

        readonly byte[] secret;

        readonly HMAC signer;

        #endregion

        /*
         * Use this constructor to create the object. The AWS credentials are available on
         * http://aws.amazon.com
         * 
         * The destination is the service end-point for your application:
         *  US: ecs.amazonaws.com
         *  JP: ecs.amazonaws.jp
         *  UK: ecs.amazonaws.co.uk
         *  DE: ecs.amazonaws.de
         *  FR: ecs.amazonaws.fr
         *  CA: ecs.amazonaws.ca
         */
        #region Constructors

        public SignedRequestHelper(string awsAccessKeyId, string awsSecretKey, string destination)
        {
            this.endPoint = destination.ToLower();
            this.akid = awsAccessKeyId;
            this.secret = Encoding.UTF8.GetBytes(awsSecretKey);
            this.signer = new HMACSHA256(this.secret);
        }

        #endregion

        /*
         * Sign a request in the form of a Dictionary of name-value pairs.
         * 
         * This method returns a complete URL to use. Modifying the returned URL
         * in any way invalidates the signature and Amazon will reject the requests.
         */
        #region Api Methods

        public XmlDocument Execute(IDictionary<string, string> request)
        {
            // Use a SortedDictionary to get the parameters in naturual byte order, as
            // required by AWS.
            var pc = new ParamComparer();
            var sortedMap = new SortedDictionary<string, string>(request, pc);

            // Add the AWSAccessKeyId and Timestamp to the requests.
            sortedMap["AWSAccessKeyId"] = this.akid;
            sortedMap["Timestamp"] = GetTimestamp();

            // Get the canonical query string
            string canonicalQS = ConstructCanonicalQueryString(sortedMap);

            // Derive the bytes needs to be signed.
            var builder = new StringBuilder();
            builder.Append(REQUEST_METHOD)
                   .Append("\n")
                   .Append(this.endPoint)
                   .Append("\n")
                   .Append(REQUEST_URI)
                   .Append("\n")
                   .Append(canonicalQS);

            string stringToSign = builder.ToString();
            var toSign = Encoding.UTF8.GetBytes(stringToSign);

            // Compute the signature and convert to Base64.
            var sigBytes = this.signer.ComputeHash(toSign);
            string signature = Convert.ToBase64String(sigBytes);

            // now construct the complete URL and return to caller.
            var qsBuilder = new StringBuilder();
            qsBuilder.Append("http://")
                     .Append(this.endPoint)
                     .Append(REQUEST_URI)
                     .Append("?")
                     .Append(canonicalQS)
                     .Append("&Signature=")
                     .Append(PercentEncodeRfc3986(signature));

            var doc = new XmlDocument();
            doc.Load(WebRequest.Create(qsBuilder.ToString())
                               .GetResponse()
                               .GetResponseStream());

            return doc;
        }

        #endregion

        /*
         * Current time in IS0 8601 format as required by Amazon
         */
        string GetTimestamp()
        {
            var currentTime = DateTime.UtcNow;
            string timestamp = currentTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            return timestamp;
        }

        /*
         * Percent-encode (URL Encode) according to RFC 3986 as required by Amazon.
         * 
         * This is necessary because .NET's HttpUtility.UrlEncode does not encode
         * according to the above standard. Also, .NET returns lower-case encoding
         * by default and Amazon requires upper-case encoding.
         */
        string PercentEncodeRfc3986(string str)
        {
            str = HttpUtility.UrlEncode(str, Encoding.UTF8);
            str = str.Replace("'", "%27").Replace("(", "%28").Replace(")", "%29").Replace("*", "%2A").Replace("!", "%21").Replace("%7e", "~").Replace("+", "%20");

            var sbuilder = new StringBuilder(str);
            for (int i = 0; i < sbuilder.Length; i++)
            {
                if (sbuilder[i] == '%')
                {
                    if (char.IsLetter(sbuilder[i + 1]) || char.IsLetter(sbuilder[i + 2]))
                    {
                        sbuilder[i + 1] = char.ToUpper(sbuilder[i + 1]);
                        sbuilder[i + 2] = char.ToUpper(sbuilder[i + 2]);
                    }
                }
            }

            return sbuilder.ToString();
        }

        /*
         * Consttuct the canonical query string from the sorted parameter map.
         */
        string ConstructCanonicalQueryString(SortedDictionary<string, string> sortedParamMap)
        {
            var builder = new StringBuilder();  
            foreach (var kvp in sortedParamMap)
            {
                builder.Append(PercentEncodeRfc3986(kvp.Key));
                builder.Append("=");
                builder.Append(PercentEncodeRfc3986(kvp.Value));
                builder.Append("&");
            }

            string canonicalString = builder.ToString();
            canonicalString = canonicalString.Substring(0, canonicalString.Length - 1);
            return canonicalString;
        }
    }

    /*
     * To help the SortedDictionary order the name-value pairs in the correct way.
     */
}