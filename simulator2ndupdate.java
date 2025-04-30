package cscorner;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.FlowLayout;

import javax.swing.JFrame;
import javax.swing.JTextField;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.util.Arrays;
import java.util.Scanner;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.Panel;

public class Window {

	private JFrame frame;
	
	private JTextField txtMemorySize;
	private	JLabel lblMemorySize;
	private JTextField txtNoOfFrames;
	private JLabel lblNoOfFrames;
	private JTextField txtNoOfJobs;
	private JLabel lblNoOfJobs;
	
	private JPanel panelRAM;
	private JPanel os;
	private JPanel panelJobTable;
	
	private JScrollPane scrollPane;

    private JTextField[] jobSizeFields;
	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					Window window = new Window();
					window.frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the application.
	 */
	public Window() {
		initialize();
	}

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		frame = new JFrame();
		frame.setBounds(100, 100, 965, 716);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.getContentPane().setLayout(null);
		frame.setLocationRelativeTo(null);
		frame.setResizable(false);
		
		txtMemorySize = new JTextField();
		txtMemorySize.setBounds(197, 36, 103, 20);
		frame.getContentPane().add(txtMemorySize);
		txtMemorySize.setColumns(10);
		
		lblMemorySize = new JLabel("Enter Memory Size:");
		lblMemorySize.setBounds(70, 42, 117, 14);
		frame.getContentPane().add(lblMemorySize);
		
		txtNoOfFrames = new JTextField();
		txtNoOfFrames.setBounds(197, 67, 103, 20);
		frame.getContentPane().add(txtNoOfFrames);
		txtNoOfFrames.setColumns(10);
		
		lblNoOfFrames = new JLabel("Enter No. of Frames: ");
		lblNoOfFrames.setBounds(70, 73, 117, 14);
		frame.getContentPane().add(lblNoOfFrames);
		
		txtNoOfJobs = new JTextField();
		txtNoOfJobs.setBounds(197, 98, 103, 20);
		frame.getContentPane().add(txtNoOfJobs);
		txtNoOfJobs.setColumns(10);
		
		lblNoOfJobs = new JLabel("Enter No. of Jobs:");
		lblNoOfJobs.setBounds(70, 101, 103, 14);
		frame.getContentPane().add(lblNoOfJobs);
		
	    JButton btnTable = new JButton("Continue");
	    btnTable.setBounds(207, 129, 89, 23);
    	btnTable.addActionListener(new ActionListener() {
    		@Override
    		public void actionPerformed(ActionEvent e) {	
				generatePanels();
				generateJobFields();
    		}
    	});
	    frame.getContentPane().add(btnTable);
    
	    panelRAM = new JPanel();
	    panelRAM.setLayout(new BoxLayout(panelRAM, BoxLayout.Y_AXIS));
	    panelRAM.setBounds(37, 164, 300, 371);
	    scrollPane = new JScrollPane(panelRAM);
	    scrollPane.setBounds(70, 170, 300, 400);
	    frame.getContentPane().add(scrollPane);
	    
	    panelJobTable = new JPanel();
	    panelJobTable.setLayout(new BoxLayout(panelJobTable, BoxLayout.Y_AXIS));
	    panelJobTable.setBounds(380, 164, 300, 371);
	    frame.getContentPane().add(panelJobTable);

	}
	
	private void generatePanels() {
		panelRAM.removeAll();
	
		try {
			double osKernel = 10, finalMemorySize, gap;
			int memorySize, noOfFrames;
			
			memorySize = Integer.parseInt(txtMemorySize.getText());
			noOfFrames = Integer.parseInt(txtNoOfFrames.getText());
			
			if (memorySize <= 0) throw new NumberFormatException();
			if (noOfFrames <= 0) throw new NumberFormatException();
			
			finalMemorySize = memorySize - osKernel;
			gap = finalMemorySize/noOfFrames;
			
			os = new JPanel();
			os.setBorder(javax.swing.BorderFactory.createLineBorder(Color.BLACK, 1));
			os.add(new JLabel("OS Kernel" + osKernel + " MB"));
			panelRAM.add(os);
			
            for (int i = 0; i < noOfFrames; i++) {
            	
                JPanel newPanel = new JPanel();
                newPanel.setPreferredSize(new Dimension(100, 20));
                newPanel.setBorder(BorderFactory.createLineBorder(Color.BLACK, 1));
               
                osKernel += gap;
                newPanel.add(new JLabel(String.format("%.1f MB | Page No: %d", osKernel, i)));
                
                panelRAM.add(newPanel);
            }
            
            panelRAM.revalidate(); // Refresh layout
            panelRAM.repaint();
            	
		}catch(NumberFormatException ex) {
            JOptionPane.showMessageDialog(null, "Please enter a valid positive number.", "Invalid Input", JOptionPane.ERROR_MESSAGE);
		}
	}
	
    private void generateJobFields() {
        panelJobTable.removeAll();
        
        try {
            int noOfJobs = Integer.parseInt(txtNoOfJobs.getText());

            if (noOfJobs <= 0) {
                throw new NumberFormatException();
            }

            jobSizeFields = new JTextField[noOfJobs];
            for (int i = 0; i < noOfJobs; i++) {
                JPanel jobPanel = new JPanel();
                jobPanel.setLayout(new FlowLayout(FlowLayout.LEFT));

                JLabel jobLabel = new JLabel("Job "+(i + 1)+ " [Arrival time (msec)]: ");
                JTextField jobSizeField = new JTextField(10);
                jobSizeFields[i] = jobSizeField;
                
                jobPanel.add(jobLabel);
                jobPanel.add(jobSizeField);
                panelJobTable.add(jobPanel);
            }

            panelJobTable.revalidate();
            panelJobTable.repaint();
            
        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(frame, "Please enter a valid positive number for jobs.", "Invalid Input", JOptionPane.ERROR_MESSAGE);
        }
    }
	
}
