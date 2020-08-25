const str =[7,0,1,2,0,3,0,4,2,3,0,3,2,1,2,0,1,7];
const framesNo = 4;

const frames = [];
for (let i = 0; i < framesNo; i++) {
  frames.push([]);
}

const getLargest = (startIndex, elements) => {
  const newArr = str.slice(startIndex);
  let largest = 0;
  let element = null;
  let count = 0;
  for (const ele of elements) {
    const i = count;
    count++;
    const eleLastIndex = newArr.lastIndexOf(ele);
    if (eleLastIndex === -1) {
      return { element: i };
    }
    if (eleLastIndex > largest) {
      largest = eleLastIndex;
      element = i;
    }
  }
  return { element };
};

let fault = 0;
let hit = 0;
let lastValues = [];
str.forEach((num, i) => {
  lastValues = [];
  for (const frame of frames) {
    if (frame[i - 1] !== undefined) {
      frame[i] = frame[i - 1];
      lastValues.push(frame[i]);
    }
    if (frame[i - 1] == undefined) {
      frame[i] = num;
      fault++;
      break;
    }
    if (lastValues.length === framesNo) {
      if (lastValues.includes(num)) {
        hit++;
      } else {
        const { element } = getLargest(i, lastValues);
        frames[element][i] = num;
        fault++;
      }
    }
  }
});

fault;
hit;
frames;
