// w3chttps://w3c.github.io/web-nfc/

window.NFCPermission = () => {
        const reader = new NDEFReader();
        reader.scan();
};

window.NFCScan = async () => {
    try {
        const reader = new NDEFReader();
        await reader.scan();
        
        reader.addEventListener("error", () => {
            DotNet.invokeMethod('WebApplication', 'UpdateMessageCaller', `Error! ${error.message}`);
        });

        reader.addEventListener("reading", ({ message, serialNumber }) => {
            var record = message.records[0];
            const textDecoder = new TextDecoder(record.encoding);
            var data = textDecoder.decode(record.data);
            DotNet.invokeMethod('WebApplication', 'UpdateMessageCaller', "Message: " + data);
        });
        return true;
    } catch (error) {
        DotNet.invokeMethod('WebApplication', 'UpdateMessageCaller', "Error " + error);
        return false;
    }
};