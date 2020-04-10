window.NFCScan = async () => {
    alert("User clicked scan button");
    console.log("User clicked scan button");
    try {
        const reader = new NDEFReader();
        await reader.scan();
        console.log("> Scan started");
        alert("> Scan started");
        
        reader.addEventListener("error", () => {
            alert(`Argh! ${error.message}`);
            log(`Argh! ${error.message}`);
        });

        reader.addEventListener("reading", ({ message, serialNumber }) => {
            alert(`> Serial Number: ${serialNumber}`);
            console.log(`> Serial Number: ${serialNumber}`);
            console.log(`> Records: (${message.records.length})`);
        });
    } catch (error) {
        console.log("Argh! " + error);
        alert("Argh! " + error);
    }
}