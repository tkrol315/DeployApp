﻿using DeployApp.Application.Abstractions;

namespace DeployApp.Application.Exceptions
{
    public class InstanceAlreadyAssignedToDeployException : DeployAppException
    {
        public InstanceAlreadyAssignedToDeployException(int deployId, int instanceId)
            : base($"Instance with id: {instanceId} already assigned to deploy with id: {deployId}", 400)
        {
        }
    }
}
