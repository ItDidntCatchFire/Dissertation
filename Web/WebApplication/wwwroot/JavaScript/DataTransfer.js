function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function readUploadedFileAsText(inputFile, type) {
    const temporaryFileReader = new FileReader();
    temporaryFileReader.onerror = () => {
        temporaryFileReader.abort();
        return false;
    };

    temporaryFileReader.addEventListener("load", function () {
        var dataB64 = temporaryFileReader.result.split(',')[1];
        var data = atob(dataB64);
        DotNet.invokeMethod('WebApplication', 'ImportJSResponse', data, type);
    }, false);

    temporaryFileReader.readAsDataURL(document.getElementById(inputFile).files[0]);
    return true;
}