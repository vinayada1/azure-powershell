﻿//  
// Copyright (c) Microsoft.  All rights reserved.
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

namespace Microsoft.Azure.Commands.ApiManagement.ServiceManagement.Commands
{
    using Microsoft.Azure.Commands.ApiManagement.ServiceManagement.Models;
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Constants.ApiManagementAuthorizationServer, DefaultParameterSetName = GetAll)]
    [OutputType(typeof(IList<PsApiManagementOAuth2AuthrozationServer>), ParameterSetName = new[] { GetAll })]
    [OutputType(typeof(PsApiManagementOAuth2AuthrozationServer), ParameterSetName = new[] { GetById })]
    public class GetAzureApiManagementAuthorizationServer : AzureApiManagementCmdletBase
    {
        private const string GetAll = "GetAllAuthorizationServers";
        private const string GetById = "GetByServerId";

        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "Instance of PsApiManagementContext. This parameter is required.")]
        [ValidateNotNullOrEmpty]
        public PsApiManagementContext Context { get; set; }

        [Parameter(
            ParameterSetName = GetById,
            ValueFromPipelineByPropertyName = true,
            Mandatory = false,
            HelpMessage = "Identifier of the authorization server. If specified will find authorization server by the identifier." +
                          " This parameter is optional. ")]
        public String ServerId { get; set; }

        public override void ExecuteApiManagementCmdlet()
        {
            if (ServerId == null)
            {
                var servers = Client.AuthorizationServerList(Context);
                WriteObject(servers, true);
            }
            else
            {
                var server = Client.AuthorizationServerById(Context, ServerId);
                WriteObject(server);
            }
        }
    }
}
