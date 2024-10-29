package org.example;
import java.io.*;
import java.nio.file.*;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.Base64;

public class Base64ToBinaryMerger {
    public static void main(String[] args) {
        // Check if the command line arguments are provided
        if (args.length < 1 || args.length > 2) {
            System.out.println("Usage: java Base64ToBinaryMerger <filePattern> [sort]");
            return;
        }

        String filePattern = args[0];
        if(filePattern.contains(".star.")){
            filePattern=filePattern.replaceAll("\\.star",".*");

        }
        boolean shouldSort = args.length == 2 && "sort".equalsIgnoreCase(args[1]);

        // Get the files matching the pattern
        try {
            //PathMatcher matcher = FileSystems.getDefault().getPathMatcher("glob:" + filePattern);
            DirectoryStream<Path> directoryStream = Files.newDirectoryStream(Paths.get("."), filePattern);

            for (Path path : directoryStream) {
               // if (matcher.matches(path.getFileName())) {
                    processFile(path.toString(), shouldSort);
               // }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static void processFile(String inputFileName, boolean shouldSort) {
        String outputFileName = inputFileName.substring(0, inputFileName.length() - 4); // Remove .txt

        try (BufferedReader reader = new BufferedReader(new FileReader(inputFileName))) {
            List<String> lines = new ArrayList<>();
            String line;

            while ((line = reader.readLine()) != null) {
                lines.add(line);
            }

            // Sort lines if requested
            if (shouldSort) {
                lines.sort(Comparator.comparing(Base64ToBinaryMerger::extractNumericValue));
            }
            StringBuilder sb= new StringBuilder();
            try (FileOutputStream fos = new FileOutputStream(outputFileName)) {
                for (String processedLine : lines) {
                    // Split the line at the first occurrence of '$'
                    int dollarIndex = processedLine.indexOf('$');
                    if (dollarIndex != -1) {
                        String base64String = processedLine.substring(dollarIndex+1);
                        System.out.println(base64String);
                        sb.append(base64String);
                        // Decode the Base64 string

                    }
                }
                writeStringToFile(outputFileName +".base64",sb.toString());
                byte[] binaryData = Base64.getDecoder().decode(sb.toString());
                // Write binary data to output file
                fos.write(binaryData);
            }

            System.out.println("Successfully merged binary data into " + outputFileName);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    private static void writeStringToFile(String filePath, String content) throws IOException {
        Path path = Paths.get(filePath);
        // Write the string to the file, creating it if it doesn't exist
        Files.write(path, content.getBytes());
    }
    private static Integer extractNumericValue(String line) {
        // Find the position of the second parameter in the line
        String[] parts = line.split("\\$");
        if (parts.length > 1) {
            Pattern pattern = Pattern.compile("\\d+");
            Matcher matcher = pattern.matcher(parts[1]); // Match against the second part
            if (matcher.find()) {
                return Integer.parseInt(matcher.group());
            }
        }
        return Integer.MAX_VALUE; // Sort lines without matching numbers to the end
    }
}
