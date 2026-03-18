#include <EnigmaRuntime.hpp>
#include <iostream>
#include <string>
int n;

int main(int argc, char* argv[])
{ 
    Application application;
    application.applicationName = APPLICATION_NAME;
    application.executableName = EXECUTABLE_NAME;
    application.creatorName = CREATOR_NAME;

    if(!application.Start())
    {
        std::cout<<"\n"<<"Application failed to start! \n";
        return 1;
    }

    while(application.IsRunning())
    {
        application.Update();
        application.FixedUpdate();
    }

    return 0;
}