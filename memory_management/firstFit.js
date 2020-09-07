const readline = require("readline");
const { exit, stdin, stdout } = require("process");

const rl = readline.createInterface({ input: stdin, output: stdout });

const getBlocks = () => {
  return new Promise((resolve, reject) => {
    rl.question("input blocks: ", (res) => {
      const process = new Map([]);
      const result = res.split(",");
      result.forEach((el, i) => {
        process.set(i + 1, Number(el));
      });

      resolve(process);
    });
  });
};

const getProcess = () => {
  return new Promise((resolve, reject) => {
    rl.question("input process: ", (res) => {
      let pArr = res.split(",");
      pArr = pArr.map((p) => Number(p));
      resolve(pArr);
    });
  });
};

// Example inputs
// blocks: 50,200,70,115,15
// process: 100,10,35,15,23,6,25,55,88,40

const main = async () => {
  const blocks = await getBlocks();
  const process = await getProcess();

  firstFit(blocks, process);
};

function firstFit(blocks, process) {
  let flag = true;
  let isExtFrag = false;

  while (flag) {
    let newFlag = false;
    const newProcess = process.shift();
    for (let block of blocks) {
      if (block[1] >= newProcess) {
        newFlag = true;
        const newSize = block[1] - newProcess;
        console.log(`block ${block[0]} = ${block[1]} - ${newProcess}`);
        blocks.set(block[0], newSize);
        console.log(blocks);
        break;
      }
    }
    if (!newFlag || !process.length) {
      flag = false;
    }

    if (!newFlag) {
      isExtFrag = true;
    }
  }

  if (isExtFrag) {
    let externalFragVal = 0;
    for (const val of blocks.values()) {
      externalFragVal += val;
    }

    console.log("external fragmentation value " + externalFragVal);
    exit();
  } else {
    console.log("No external fragmentation");
    exit();
  }
}

main();
