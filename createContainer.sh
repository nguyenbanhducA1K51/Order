#!/bin/bash

envs=$(awk -F= 'NF==2 {
  # Remove any CR characters
  gsub(/\r/, "");
  printf "%s ", $0
}' .env)

# Create the container with these variables
az container create \
  --resource-group Antra \
  --name orderservice \
  --image antraregistry.azurecr.io/project1/orderservice \
  --environment-variables $envs