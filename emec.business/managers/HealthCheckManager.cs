/*------------------------------------------------------------------------------
© 2025 Jayashan Liyanage. All rights reserved.
File: $fileName$
Author: $user$
Created: $time$
Description: [Add file description here]
------------------------------------------------------------------------------
All Rights Reserved.
     This unpublished material is proprietary to Jayashan Liyanage. The methods and techniques described herein are considered trade secrets (copyright) and/or confidential.
     Reproduction or distribution, in whole or in part, is forbidden except by prior express written permission from Jayashan Liyanage.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.contracts.repositories;
using emec.shared.models;
using emec.entities.HealthCheck;
using emec.contracts.managers;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace emec.business.managers
{
    public class HealthCheckManager : IHealthCheckManager
    {
        private readonly IHealthCheckRepository _healthcheckRepository;

        private readonly IMapper<Object, ResponseBase> _serviceResponseMapper;

        private readonly IValidator<HealthCheckDataRequest> _healthCheckDataRequestValidator;

        private readonly IMapper<ResponseMessage, ResponseBase> _serviceResponseErrorMapper;

        public HealthCheckManager(
            IHealthCheckRepository healthcheckRepository,
            IMapper<ResponseMessage, ResponseBase> serviceResponseErrorMapper,
            IValidator<HealthCheckDataRequest> healthCheckDataRequestValidator,
            IMapper<Object, ResponseBase> serviceResponseMapper
            )
        {
            _healthcheckRepository = healthcheckRepository ?? throw new ArgumentNullException(nameof(healthcheckRepository));
            _serviceResponseMapper = serviceResponseMapper ?? throw new ArgumentNullException(nameof(serviceResponseMapper));
            _healthCheckDataRequestValidator = healthCheckDataRequestValidator ?? throw new ArgumentNullException(nameof(healthCheckDataRequestValidator));
            _serviceResponseErrorMapper = serviceResponseErrorMapper ?? throw new ArgumentNullException(nameof(serviceResponseErrorMapper));
        }

        public async Task<ResponseBase> GetHealth(HealthCheckDataRequest healthCheckDataRequest)
        {
            if (!_healthCheckDataRequestValidator.Validate(healthCheckDataRequest, out ResponseMessage message))
            {
                return _serviceResponseErrorMapper.Map(message);
            }
            else
            {
                var locations = await _healthcheckRepository.GetHealthCheckDataAsync(healthCheckDataRequest);
                return _serviceResponseMapper.Map(locations);
            }
        }
    }
}