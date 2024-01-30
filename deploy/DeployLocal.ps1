$kubeseal = "C:/Apps/kubeseal/kubeseal"
$name "testdemo"
$semanticVersion = "latest"

cd ./${deploy}/${name}

kustomize edit set image "${env:REGISTRYHOST}/${name}:${semanticVersion}"

if(Test-Path -Path ./secrets/*)
{
	"Creating secrets"
	kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
		&${kubeseal} -n "${namespace}" --controller-namespace kube-system --format yaml > "secret.yaml"
}

cd ../..

kubectl apply -k ./${deploy}/${name}
	
#Restore secret.yaml and kustomization.yaml since this script alters them temporary
#if([string]::IsNullOrEmpty($env:AGENT_NAME) -and [string]::IsNullOrEmpty($hostname))
if([string]::IsNullOrEmpty($env:AGENT_NAME))
{
	if(Test-Path -Path ${deploy}/${name}/secret.yaml)
	{
		git checkout ${deploy}/${name}/secret.yaml
	}
	git checkout ${deploy}/${name}/kustomization.yaml
}
