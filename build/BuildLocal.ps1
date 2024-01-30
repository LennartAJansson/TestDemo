$name = "testdemo"
$lowerName = $name.ToLower()
$semanticVersion = "latest"
docker build -f ./${name}/Dockerfile --force-rm -t ${env:REGISTRYHOST}/${lowerName}:${semanticVersion} .
docker push ${env:REGISTRYHOST}/${lowerName}:${semanticVersion}
