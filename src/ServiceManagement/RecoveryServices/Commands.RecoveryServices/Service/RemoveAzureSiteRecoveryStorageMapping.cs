﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.RecoveryServices.SiteRecovery;
using Microsoft.WindowsAzure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.RecoveryServices
{
    /// <summary>
    /// Removes Azure Site Recovery Storage mapping.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureSiteRecoveryStorageMapping")]
    [OutputType(typeof(ASRJob))]
    [Obsolete("This cmdlet has been marked for deprecation in an upcoming release. Please use the " +
        "equivalent cmdlet from the AzureRm.RecoveryServices.SiteRecovery module instead.",
        false)]
    public class RemoveAzureSiteRecoveryStorageMapping : RecoveryServicesCmdletBase
    {
        #region Parameters
        /// <summary>
        /// Job response.
        /// </summary>
        private JobResponse jobResponse = null;

        /// <summary>
        /// Gets or sets Storage mapping object.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRStorageMapping StorageMapping { get; set; }
        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            try
            {
                this.WriteWarningWithTimestamp(
                    string.Format(
                        Properties.Resources.CmdletWillBeDeprecatedSoon,
                        this.MyInvocation.MyCommand.Name));

                this.jobResponse =
                    RecoveryServicesClient
                    .RemoveAzureSiteRecoveryStorageMapping(
                    this.StorageMapping.PrimaryServerId,
                    this.StorageMapping.PrimaryStorageId,
                    this.StorageMapping.RecoveryServerId,
                    this.StorageMapping.RecoveryStorageId);

                this.WriteJob(this.jobResponse.Job);
            }
            catch (Exception exception)
            {
                this.HandleException(exception);
            }
        }

        /// <summary>
        /// Writes Job.
        /// </summary>
        /// <param name="job">JOB object</param>
        private void WriteJob(Microsoft.WindowsAzure.Management.SiteRecovery.Models.Job job)
        {
            this.WriteObject(new ASRJob(job));
        }
    }
}