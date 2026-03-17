#include <iostream>
#include <EnigmaRuntime.hpp>
int n;

int main(int argc, char* argv[])
{ 
    while(1>0)
    {
        Application application;
        application.applicationName = APPLICATION_NAME;
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
    }

    return 0;
}