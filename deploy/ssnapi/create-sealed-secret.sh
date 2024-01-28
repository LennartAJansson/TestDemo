#!/bin/bash
set -x

for file in ./secrets/*; do
    filename=$(basename -- "$file")
    filename="${filename%.*}"
    
    key=$(echo $filename)

    secrets="${secrets} --from-file=$key=$file"
done

kubectl create secret generic testdemo-secret --output json --dry-run=client ${secrets} | \
    C:/Apps/kubeseal/kubeseal \
    -n "testdemo" \
    --controller-namespace kube-system \
    --format yaml > "secret.yaml"