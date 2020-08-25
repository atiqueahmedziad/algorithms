const str =[7,0,1,2,0,3,0,4,2,3,0,3,2,1,2,0,1,7,0,1];
const framesNo = 3;

const frames = [];
for (let i = 0; i < framesNo; i++) {
  frames.push([]);
}

const getLargest = (endIndex, elements) => {
  const newArr = str.slice(0,endIndex);
  let smallest = newArr.length;
  let element = null;
  let count = 0;
  for (const ele of elements) {
    const i = count;
    count++;
    let eleLastIndex;
    for(let i=newArr.length-1; i>=0; i--){
      if(newArr[i] === ele){
        eleLastIndex = i;
        break;
      }
    }
    if (eleLastIndex === -1) {
      return { element: i };
    }
    if (eleLastIndex < smallest) {
      smallest = eleLastIndex;
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
