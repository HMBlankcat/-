import numpy as np
import tkinter as tk
from tkinter import messagebox


class AHP:
    """
    层次分析法（AHP）类，用于处理判断矩阵的权重计算和一致性检验。
    """

    def __init__(self, array, ri_value):
        ## 记录矩阵相关信息
        self.array = array
        ## 记录矩阵大小
        self.n = array.shape[0]
        # 初始化RI值，用于一致性检验
        self.RI_value = ri_value
        # 矩阵的特征值和特征向量
        self.eig_val, self.eig_vector = np.linalg.eig(self.array)
        # 矩阵的最大特征值
        self.max_eig_val = np.max(self.eig_val)
        # 矩阵最大特征值对应的特征向量
        self.max_eig_vector = self.eig_vector[:, np.argmax(self.eig_val)].real
        # 矩阵的一致性指标CI
        self.CI_val = (self.max_eig_val - self.n) / (self.n - 1)
        # 矩阵的一致性比例CR
        self.CR_val = self.CI_val / self.RI_value if self.RI_value != 0 else 0

    """
    一致性判断
    """

    def test_consist(self):
        # 进行一致性检验判断
        if self.n == 2:  # 当只有两个子因素的情况
            return True
        else:
            return self.CR_val < 0.1  # CR值小于0.1，可以通过一致性检验

    """
    算术平均法求权重
    """

    def cal_weight_by_arithmetic_method(self):
        # 求矩阵的每列的和
        col_sum = np.sum(self.array, axis=0)
        # 将判断矩阵按照列归一化
        array_normed = self.array / col_sum
        # 计算权重向量
        array_weight = np.sum(array_normed, axis=1) / self.n
        return array_weight


# GUI Application
def main():
    def on_calculate():
        try:
            matrices_str = matrices_text.get("1.0", tk.END).strip()
            matrices_blocks = matrices_str.split("\n\n")
            ri_values_str = entry_ri.get().strip()
            ri_values = list(map(float, ri_values_str.strip("[]").split(",")))
            results = ""
            weights_list = []

            for i, matrix_str in enumerate(matrices_blocks):
                rows = matrix_str.splitlines()
                matrix = np.array([list(map(float, row.split())) for row in rows])

                if matrix.shape[0] != matrix.shape[1]:
                    messagebox.showerror("输入错误", f"矩阵 {i + 1} 必须是方阵！")
                    return

                n = matrix.shape[0]
                if n > len(ri_values):
                    messagebox.showerror("输入错误",
                                         f"矩阵 {i + 1} 的大小超出了RI值的范围（最大支持 {len(ri_values)}x{len(ri_values)} 矩阵）。")
                    return

                ri_value = ri_values[n - 1]
                ahp = AHP(matrix, ri_value)

                if ahp.test_consist():
                    weights = ahp.cal_weight_by_arithmetic_method()
                    weights_list.append(weights)
                    results += f"矩阵 {i + 1} 一致性检验通过！\nCR值：{ahp.CR_val:.4f}\n权重：{weights}\n\n"
                else:
                    results += f"矩阵 {i + 1} 一致性检验未通过！\nCR值：{ahp.CR_val:.4f}\n请调整判断矩阵。\n\n"
                    result_label.config(text=results)
                    return

            # 计算最终的总权重
            if len(weights_list) > 1:
                final_weights = weights_list[0]
                for i in range(1, len(weights_list)):
                    final_weights = final_weights + weights_list[i] * weights_list[0][i]

            result_label.config(text=results)
        except ValueError:
            messagebox.showerror("输入错误", "请输入有效的矩阵和RI值！")

    # Setup tkinter window
    window = tk.Tk()
    window.title("AHP一致性检验与权重计算工具")

    # Matrices Input Label and Textbox
    tk.Label(window, text="请输入判断矩阵（多个矩阵之间用空行分隔）：").pack(pady=5)
    matrices_text = tk.Text(window, height=15, width=70)
    matrices_text.pack(pady=5)

    # RI Values Entry
    tk.Label(window, text="请输入随机一致性指标（RI）值列表，例如：[0, 0, 0.58, 0.90, 1.12, ...]：").pack(pady=5)
    entry_ri = tk.Entry(window, width=70)
    entry_ri.pack(pady=5)

    # Calculate Button
    calculate_button = tk.Button(window, text="计算CR、检验一致性、计算权重", command=on_calculate)
    calculate_button.pack(pady=10)

    # Result Label
    result_label = tk.Label(window, text="")
    result_label.pack(pady=10)

    # Run the application
    window.mainloop()


if __name__ == "__main__":
    main()

    '''
    1.000 3.000 5.000 2.000 4.000 7.000 6.000 8.000 8.000 8.000 8.000 9.000 9.000 9.000
0.333 1.000 3.000 0.500 2.000 5.000 4.000 6.000 9.000 8.000 7.000 8.000 9.000 9.000
0.200 0.333 1.000 0.250 0.500 3.000 2.000 4.000 9.000 6.000 5.000 7.000 8.000 9.000
0.500 2.000 4.000 1.000 3.000 6.000 5.000 7.000 9.000 8.000 8.000 9.000 9.000 9.000
0.250 0.500 2.000 0.333 1.000 4.000 3.000 5.000 9.000 7.000 6.000 8.000 8.000 9.000
0.143 0.200 0.333 0.167 0.250 1.000 0.500 2.000 7.000 4.000 3.000 5.000 6.000 8.000
0.167 0.250 0.500 0.200 0.333 2.000 1.000 3.000 8.000 5.000 4.000 6.000 7.000 9.000
0.125 0.167 0.250 0.143 0.200 0.500 0.333 1.000 6.000 3.000 2.000 4.000 5.000 7.000
0.125 0.111 0.111 0.111 0.111 0.143 0.167 0.500 1.000 0.250 0.200 0.333 0.500 2.000
0.125 0.125 0.167 0.111 0.143 0.200 0.250 0.250 0.250 1.000 0.500 2.000 3.000 5.000
0.125 0.143 0.200 0.125 0.167 0.333 0.250 0.500 5.000 2.000 1.000 3.000 4.000 6.000
0.111 0.125 0.143 0.111 0.125 0.200 0.167 0.250 0.333 0.500 0.333 1.000 2.000 4.000
0.111 0.111 0.125 0.111 0.125 0.167 0.143 0.200 0.500 0.333 0.250 0.500 1.000 3.000
0.111 0.111 0.111 0.111 0.111 0.125 0.111 0.143 0.500 0.200 0.167 0.250 0.333 1.000

1.000 2.000 3.000 4.000 5.000 9.000 6.000 9.000 9.000 9.000 7.000 8.000 8.000 9.000
0.500 1.000 2.000 3.000 4.000 8.000 5.000 9.000 9.000 9.000 6.000 7.000 8.000 9.000
0.333 0.500 1.000 2.000 3.000 8.000 4.000 9.000 9.000 8.000 5.000 6.000 7.000 9.000
0.250 0.333 0.500 1.000 2.000 7.000 3.000 9.000 8.000 8.000 4.000 5.000 6.000 9.000
0.200 0.250 0.333 0.500 1.000 6.000 2.000 9.000 8.000 7.000 3.000 4.000 5.000 8.000
0.111 0.125 0.125 0.143 0.167 1.000 0.200 5.000 3.000 2.000 0.250 0.333 0.500 4.000
0.167 0.200 0.250 0.333 0.500 5.000 1.000 9.000 7.000 6.000 2.000 3.000 8.000 4.000
0.111 0.111 0.111 0.111 0.111 0.200 0.111 1.000 0.333 0.250 0.125 0.143 0.167 2.000
0.111 0.111 0.111 0.125 0.125 0.333 0.143 3.000 1.000 0.500 0.167 0.200 0.250 2.000
0.111 0.111 0.125 0.125 0.143 0.500 0.167 4.000 2.000 1.000 0.200 0.250 0.333 3.000
0.143 0.167 0.200 0.250 0.333 4.000 0.500 8.000 6.000 5.000 1.000 2.000 3.000 7.000
0.125 0.143 0.167 0.200 0.250 3.000 0.333 7.000 5.000 4.000 0.500 1.000 2.000 8.000
0.125 0.125 0.143 0.167 0.200 0.500 0.125 6.000 4.000 3.000 0.333 0.500 1.000 5.000
0.111 0.111 0.111 0.111 0.125 0.250 0.250 0.500 0.500 0.333 0.143 0.125 0.200 1.000

1.000 6.000 8.000 5.000 2.000 7.000 8.000 3.000 9.000 4.000 9.000 9.000 9.000 9.000
0.167 1.000 4.000 0.500 0.200 2.000 3.000 0.250 8.000 0.333 5.000 7.000 6.000 9.000
0.125 0.250 1.000 0.200 0.125 0.333 0.500 0.143 5.000 0.167 2.000 4.000 6.000 3.000
0.200 2.000 5.000 1.000 0.250 3.000 4.000 0.333 9.000 0.500 6.000 8.000 9.000 7.000
0.500 5.000 8.000 4.000 1.000 6.000 7.000 2.000 3.000 8.000 9.000 9.000 9.000 9.000
0.143 0.500 3.000 0.333 0.167 1.000 2.000 0.200 7.000 0.250 4.000 5.000 7.000 5.000
0.125 0.333 0.500 0.250 0.143 0.500 1.000 0.167 6.000 0.200 3.000 5.000 7.000 4.000
0.333 4.000 7.000 3.000 0.500 5.000 6.000 1.000 9.000 0.500 8.000 9.000 9.000 8.000
0.111 0.125 0.200 0.111 0.333 0.143 0.167 0.111 1.000 0.333 0.250 0.500 0.333 2.000
0.250 3.000 6.000 2.000 0.125 4.000 5.000 2.000 3.000 1.000 7.000 9.000 9.000 8.000
0.111 0.200 0.500 0.167 0.111 0.250 0.333 0.125 4.000 0.143 1.000 3.000 5.000 2.000
0.111 0.143 0.250 0.125 0.111 0.200 0.200 0.111 0.500 0.111 0.333 1.000 3.000 0.500
0.111 0.167 0.167 0.111 0.111 0.143 0.143 0.111 0.333 0.111 0.200 0.333 1.000 0.250
0.111 0.111 0.333 0.143 0.111 0.200 0.250 0.125 0.500 0.125 0.500 2.000 4.000 1.000


[0, 0, 0.58, 0.90, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49, 1.52, 1.54, 1.56, 1.58, 1.59]
    '''