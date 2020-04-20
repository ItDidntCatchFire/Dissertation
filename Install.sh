sudo apt-get update
sudo apt-get upgrade

#Get the files
wget https://download.visualstudio.microsoft.com/download/pr/349f13f0-400e-476c-ba10-fe284b35b932/44a5863469051c5cf103129f1423ddb8/dotnet-sdk-3.1.102-linux-arm.tar.gz
wget https://download.visualstudio.microsoft.com/download/pr/8ccacf09-e5eb-481b-a407-2398b08ac6ac/1cef921566cb9d1ca8c742c9c26a521c/aspnetcore-runtime-3.1.2-linux-arm.tar.gz

#Move files
mkdir dotnet-arm32
tar zxf dotnet-sdk-3.1.102-linux-arm.tar.gz -C $HOME/dotnet-arm32
tar zxf aspnetcore-runtime-3.1.2-linux-arm.tar.gz -C $HOME/dotnet-arm32

#Export
export DOTNET_ROOT=$HOME/dotnet-arm32
export PATH=$PATH:$HOME/dotnet-arm32

#JSON parser
sudo apt-get install jq

cd Documents

#Set up ngrok
wget https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-linux-arm.zip
unzip ngrok-stable-linux-arm.zip
./ngrok authtoken 1alB2cv4Z9c6zkxNR7C94fIlClp_2YhpHjJpNBPdZecdnNQ1d


#Download Project
git clone https://github.com/ItDidntCatchFire/Dissertation
cd Dissertation

#Start Api
cd API
dotnet publish -c Release --self-contained -r linux-arm
cd bin/Release/netcoreapp3.1/linux-arm/publish
nohup dotnet API.dll --urls "https://$(ip addr show wlp36s0 | grep -Po 'inet \K[\d.]+'):5321" > /dev/null 2>&1 &
cd ../../../../../../../
./ngrok http https://localhost:5321

#Save the URL
cd Dissertation/Web/WebApplication/wwwroot
rm ip.json
#Get ip from ngrok
echo "\"$(curl -s localhost:4040/api/tunnels | jq -r .tunnels[0].public_url)/\"" >>ip.json
#Get IP if on device
#echo "\"https://$(ip addr show wlan0 | grep -Po 'inet \K[\d.]+'):5321/\"" >> ip.json

#Start the website
cd ..
dotnet publish -c Release --self-contained -r linux-arm
#nohup dotnet run -v q --urls https://0.0.0.0:5322 > /dev/null 2>&1 &

cd bin/Release/netstandard2.0/linux-arm/publish
nohup dotnet run WebApplication --urls "https://$(ip addr show wlp36s0 | grep -Po 'inet \K[\d.]+'):5322" > /dev/null 2>&1 &


