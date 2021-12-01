function draw(arr, retArr) {
    let canvas = document.getElementById('canvas');

    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // //точки
        for (let i = 0; i < arr.length; i++) {

            let temp = arr[i];
            console.log(temp);
            ctx.fillStyle = 'rgb(200, 0, 0)';
            ctx.beginPath();
            ctx.arc(temp.x, temp.y, 5, 0, 2 * Math.PI);
            ctx.fill();
        }

        //ребра
        for (let i = 0; i < arr.length; i++) {

            let temp = arr[i];

            for (let j = 0; j < retArr[i].length; j++) {
                if (retArr[i][j] > 0 && i != j && (retArr[i][j] == retArr[j][i] || retArr[j][i] == 0)) {
                    let tempForEdge = arr[j];

                    ctx.beginPath();
                    canvas_arrow(ctx, temp.x, temp.y, tempForEdge.x, tempForEdge.y);
                    ctx.stroke();

                    let x_c = (temp.x + tempForEdge.x) / 2;
                    let y_c = (temp.y + tempForEdge.y) / 2;

                    ctx.font = "18px Verdana";
                    ctx.fillStyle = 'rgb(0, 0, 0)';
                    ctx.fillText(retArr[i][j], x_c, y_c);
                }

                if (retArr[i][j] > 0 && i != j && retArr[i][j] != retArr[j][i] && retArr[j][i] > 0 && i < j) {
                    let tempForEdge = arr[j];

                    let x_c = (temp.x + tempForEdge.x) / 2;
                    let y_c = (temp.y + tempForEdge.y) / 2;

                    ctx.beginPath();
                    ctx.moveTo(temp.x, temp.y);
                    ctx.lineTo(x_c + 50, y_c + 50);
                    ctx.stroke();

                    ctx.beginPath();
                    canvas_arrow(ctx, x_c + 50, y_c + 50, tempForEdge.x, tempForEdge.y);
                    ctx.stroke();

                    ctx.font = "18px Verdana";
                    ctx.fillStyle = 'rgb(0, 0, 0)';
                    ctx.fillText(retArr[i][j], x_c + 50, y_c + 50);

                    ctx.beginPath();
                    ctx.moveTo(tempForEdge.x, tempForEdge.y);
                    ctx.lineTo(x_c - 50, y_c - 50);
                    ctx.stroke();

                    ctx.beginPath();
                    canvas_arrow(ctx, x_c - 50, y_c - 50, temp.x, temp.y);
                    ctx.stroke();

                    ctx.font = "18px Verdana";
                    ctx.fillStyle = 'rgb(0, 0, 0)';
                    ctx.fillText(retArr[j][i], x_c - 50, y_c - 50);
                }

                if (retArr[i][j] > 0 && i == j) {
                    ctx.beginPath();
                    ctx.arc(temp.x - 20, temp.y + 5, 20, 0, Math.PI * 1.9);
                    ctx.stroke();

                    ctx.font = "18px Verdana";
                    ctx.fillStyle = 'rgb(66, 66, 66)';
                    ctx.fillText(retArr[i][j], temp.x - 20, temp.y + 25);
                }
            }
        }

        //названия точек
        for (let i = 0; i < arr.length; i++) {

            let temp = arr[i];
            ctx.font = "18px Verdana";
            ctx.fillStyle = 'rgb(0, 0, 0)';
            ctx.fillText(temp.name, temp.x + 10, temp.y);
        }

    }
}

function canvas_arrow(context, fromx, fromy, tox, toy) {
    let headlen = 10; // length of head in pixels
    let dx = tox - fromx;
    let dy = toy - fromy;
    let angle = Math.atan2(dy, dx);
    context.moveTo(fromx, fromy);
    context.lineTo(tox, toy);
    context.lineTo(tox - headlen * Math.cos(angle - Math.PI / 6), toy - headlen * Math.sin(angle - Math.PI / 6));
    context.moveTo(tox, toy);
    context.lineTo(tox - headlen * Math.cos(angle + Math.PI / 6), toy - headlen * Math.sin(angle + Math.PI / 6));
}

window.onload = function () {

    const table = document.getElementById('main_tb_container');
    const btnStart = document.getElementById('startButton');
    const btnClear = document.getElementById('clearBtn');
    const btnDraw = document.getElementById('drawBtn');
    const btnDFS = document.getElementById('dfsBtn');
    const arrNode = [];

    function clearTable() {
        const context = canvas.getContext('2d');

        context.clearRect(0, 0, canvas.width, canvas.height);
    }

    btnClear.onclick = function () {
        document.getElementById("drawBtn").disabled = true;

        for (i = table.tBodies[0].rows.length - 1; i >= 0; i--) { table.tBodies[0].deleteRow(i); }

        clearTable();
    }

    function create_node(_x, _y, _id, _name) {
        return {
            x: _x,
            y: _y,
            id: _id,
            name: _name
        }
    }

    function counting_nodes(n, retArr) {
        let currentAngle = 0; // текущее значение угла
        let radius = 150; // радиус окружности
        let baseX = 590; // x координата центра окружности
        let baseY = 200; // y координата центра окружностиF

        let grad = 360 / (n + 1);

        for (let i = 0; i < n; i++) {

            let vx = Math.cos(currentAngle) * radius;
            let vy = Math.sin(currentAngle) * radius;

            arrNode[i] = create_node(baseX + vx, baseY + vy, i, i);

            currentAngle += grad;
            //console.log(currentAngle);
        }

        //console.log(arrNode);
        draw(arrNode, retArr);
    }

    function get_nums() {
        let numNodes = document.getElementById('numNodes').value;
        let num = parseInt(numNodes);

        return num;
    }

    function create_table(myRows, myCols) {

        console.log(table.rows.length);
        if (table.rows.length > 0) {
            for (i = table.tBodies[0].rows.length - 1; i >= 0; i--) {
                table.tBodies[0].deleteRow(i);
            }
        }

        let rowHead = table.insertRow(-1);
        let cell = rowHead.insertCell(-1);
        cell.innerHTML = " ";
        cell.setAttribute("contenteditable", "false");

        for (let j = 0; j < myCols; j++) {
            let cell = rowHead.insertCell(-1);
            cell.innerHTML = j;
            cell.setAttribute("contenteditable", "false");
        }

        for (let i = 0; i < myRows; i++) {
            let row = table.insertRow(-1);

            let cellHead = row.insertCell(-1);
            cellHead.innerHTML = i;
            cellHead.setAttribute("contenteditable", "false");

            for (let j = 1; j < myCols + 1; j++) {
                let cell = row.insertCell(-1);
                cell.innerHTML = 0;
            }
        }
    }

    btnStart.onclick = function () {
        document.getElementById("drawBtn").disabled = false;
        document.getElementById("dfsBtn").disabled = false;
        let num = get_nums();
        create_table(num, num)
    }

    function save_and_return_arr() {
        let num = get_nums();

        console.log(table)

        let valInputTable = new Array(num);
        for (let i = 0; i < valInputTable.length; i++) {
            valInputTable[i] = new Array(num);
        }

        for (let i = 0; i < valInputTable.length; i++) {
            for (let j = 0; j < valInputTable.length; j++) {
                valInputTable[i][j] = parseInt(table.rows[i + 1].cells[j + 1].innerHTML)
            }
        }
        return valInputTable;
    }

    btnDraw.onclick = function () {
        clearTable();

        let num = get_nums();
        let retArr = save_and_return_arr();
        counting_nodes(num, retArr);
    }

    function dfsElem(num, node, retArr, used)
    {
        used.push(node);
        for (let i = 0; i < retArr[node].length; i++) {
            if (retArr[node][i] > 0 && i != node && used.indexOf(i) == -1) {
                used = dfs(num, i, retArr, used);
            }
        }
    }

    function dfs(num, node, retArr, used) {

        used.push(node);
        for (let i = 0; i < retArr[node].length; i++) {
            if (retArr[node][i] > 0 && i != node && used.indexOf(i) == -1) {
                dfs(num, i, retArr, used);
            }
        }

        if (used.length < num && node == 0) {
            for (let i = 0; i < num; i++) {
                if (used.indexOf(i) == -1) {
                    dfs(num, i, retArr, used);
                }
            }
            
            console.log(used);
        }

    }

    btnDFS.onclick = function () {

        let num = get_nums();
        let retArr = save_and_return_arr();

        dfs(num, 0, retArr, []);
    }
}