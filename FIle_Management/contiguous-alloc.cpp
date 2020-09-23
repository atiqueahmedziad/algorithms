#include <iostream>
#include <vector>

using namespace std;

int countFileSize = 0;
int totalBlock;

class File{
    public:
        string fileName;
        int fileSize;
        string blocks;
};

string createFile(File newFile) {
    if(newFile.fileSize > totalBlock) {
        cout << "File "+ newFile.fileName +" cannot be created" << "\n";
        return "";
    } else {
        totalBlock -= newFile.fileSize;
        int init = countFileSize;
        countFileSize += newFile.fileSize;
        for(int i=init; i<countFileSize; i++) {
            newFile.blocks += to_string(i+1) + " ";
        }
        cout << "File "+ newFile.fileName +" is created" << "\n";
        return newFile.blocks;
    }
}

int main() {
    cout << "block size" << "\n";
    cin >> totalBlock;

    int n;
    cout << "number of files want to create: "<< "\n";
    cin >> n;

    vector<File> files;

    for(int i=0; i<n; i++) {
        File newFile;
        cout << "File Name: "<< "\n";
        cin >> newFile.fileName;
        cout << "File Size: "<< "\n";
        cin >> newFile.fileSize;

        newFile.blocks = createFile(newFile);

        files.push_back(newFile);
    }

    cout << "Search by file name: "<< "\n";
    string searchStr;

    cin >> searchStr;

    int flag = 0;
    File foundBlocks;

    for(File f: files){
        if(f.fileName.compare(searchStr) == 0) {
            flag = 1;
            foundBlocks = f;
        }
    }

    if(flag == 0) {
        cout << "NA";
    } else if(flag ==1) {
        cout << foundBlocks.fileName + " has blocks: "+ foundBlocks.blocks << endl;
    }

}
