// w3chttps://w3c.github.io/web-nfc/

window.NFCScan = async () => {
    try {
        const reader = new NDEFReader();
        await reader.scan();
        
        reader.addEventListener("error", () => {
            DotNet.invokeMethod('BlazorSample', 'UpdateMessageCaller', `Error! ${error.message}`);
        });

        reader.addEventListener("reading", ({ message, serialNumber }) => {
            //console.log(`> Serial Number: ${serialNumber}`);
            //console.log(`> Records: (${message.records.length})`);

            DotNet.invokeMethod('WebApplication', 'UpdateMessageCaller', "Serial Number: " + serialNumber);
        });
    } catch (error) {
        DotNet.invokeMethod('WebApplication', 'UpdateMessageCaller', "Error " + error);
    }
};