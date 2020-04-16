// w3chttps://w3c.github.io/web-nfc/

window.NFCScan = async () => {
    try {
        const reader = new NDEFReader();
        await reader.scan();
        
        reader.addEventListener("reading", ({ message, serialNumber }) => {
            var record = message.records[0];
            const textDecoder = new TextDecoder(record.encoding);
            var data = textDecoder.decode(record.data);
            
            DotNet.invokeMethod('WebApplication', 'UpdateMessageCaller', data);
        });
        
        return true;
    } catch (error) {
        return false;
    }
};