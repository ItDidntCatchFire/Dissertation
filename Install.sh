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

cd Documents

#Download Project
git clone https://github.com/ItDidntCatchFire/Dissertation
cd Dissertation

Run.sh


#Set up ngrok
wget https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-linux-arm.zip
unzip ngrok-stable-linux-arm.zip
./ngrok authtoken 1alB2cv4Z9c6zkxNR7C94fIlClp_2YhpHjJpNBPdZecdnNQ1d
